using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoLogistica
{
    public partial class Frm_orcamento : Form
    {
        // ==========================================
        // VARIÁVEL DO KM (use KmCalculado no orçamento)
        // ==========================================
        private double kmCalculado;
        public double KmCalculado { get { return kmCalculado; } }

        // Resultado da busca de coordenadas
        private class Ponto
        {
            public double Lat;
            public double Lon;
            public bool Aproximado; // true = achou só a cidade, não a rua
        }

        private DateTime _ultimaConsultaNominatim = DateTime.MinValue;
        private string _ultimoErro = "";

        public Frm_orcamento()
        {
            // Necessário no .NET 4.7.2 para HTTPS funcionar em todas as APIs
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            InitializeComponent();
        }

        private async void Frm_orcamento_Load(object sender, EventArgs e)
        {
            // Inicializa o motor do WebView2
            await webView21.EnsureCoreWebView2Async(null);
        }

        // ==========================================
        // BUSCA DE CEP - ORIGEM
        // ==========================================
        private async void txtCepOrigem_Leave(object sender, EventArgs e)
        {
            await BuscarCep(txtCepOrigem.Text, "Origem", txtRuaO, txtBairroO, textCidadeO, textUFO);
        }

        // ==========================================
        // BUSCA DE CEP - DESTINO
        // ==========================================
        private async void txtCepDestino_Leave(object sender, EventArgs e)
        {
            await BuscarCep(txtCepDestino.Text, "Destino", txtRuafim, txtBairroFim, textCidadeFim, textUFFim);
        }

        private async Task BuscarCep(string cepTexto, string rotulo,
                                     Control rua, Control bairro, Control cidade, Control uf)
        {
            string cep = Regex.Replace(cepTexto ?? "", "[^0-9]", "");
            if (cep.Length != 8) return;

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    string resposta = await client.DownloadStringTaskAsync($"https://viacep.com.br/ws/{cep}/json/");

                    if (Regex.IsMatch(resposta, "\"erro\"\\s*:\\s*true"))
                    {
                        MessageBox.Show($"CEP de {rotulo} não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    rua.Text = ExtrairValorJson(resposta, "logradouro");
                    bairro.Text = ExtrairValorJson(resposta, "bairro");
                    cidade.Text = ExtrairValorJson(resposta, "localidade");
                    uf.Text = ExtrairValorJson(resposta, "uf");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar o CEP de {rotulo}: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // LEITURA DE JSON (texto entre aspas e número)
        // ==========================================
        private string ExtrairValorJson(string json, string chave)
        {
            try
            {
                string busca = $"\"{chave}\":\"";
                int inicio = json.IndexOf(busca);
                if (inicio == -1)
                {
                    busca = $"\"{chave}\": \"";
                    inicio = json.IndexOf(busca);
                    if (inicio == -1) return "";
                }
                inicio += busca.Length;
                int fim = json.IndexOf("\"", inicio);
                return json.Substring(inicio, fim - inicio);
            }
            catch
            {
                return "";
            }
        }

        private string ExtrairNumeroJson(string json, string chave)
        {
            var m = Regex.Match(json, "\"" + Regex.Escape(chave) + "\"\\s*:\\s*(-?[0-9]+(\\.[0-9]+)?)");
            return m.Success ? m.Groups[1].Value : "";
        }

        // ==========================================
        // COORDENADAS (Nominatim), com tentativas em etapas
        // ==========================================
        private async Task<Ponto> ObterPontoComFallback(string rua, string bairro, string cidade, string uf)
        {
            rua = (rua ?? "").Trim();
            bairro = (bairro ?? "").Trim();
            cidade = (cidade ?? "").Trim();
            uf = (uf ?? "").Trim();

            var tentativas = new List<KeyValuePair<string, bool>>();

            if (rua != "" && bairro != "")
                tentativas.Add(new KeyValuePair<string, bool>($"{rua}, {bairro}, {cidade}, {uf}, Brasil", false));
            if (rua != "")
                tentativas.Add(new KeyValuePair<string, bool>($"{rua}, {cidade}, {uf}, Brasil", false));
            tentativas.Add(new KeyValuePair<string, bool>($"{cidade}, {uf}, Brasil", true));

            foreach (var t in tentativas)
            {
                Ponto p = await ObterCoordenadas(t.Key);
                if (p != null)
                {
                    p.Aproximado = t.Value;
                    return p;
                }
            }
            return null;
        }

        private async Task<Ponto> ObterCoordenadas(string endereco)
        {
            try
            {
                // Nominatim permite no máximo 1 consulta por segundo
                double esperar = 1100 - (DateTime.Now - _ultimaConsultaNominatim).TotalMilliseconds;
                if (esperar > 0) await Task.Delay((int)esperar);
                _ultimaConsultaNominatim = DateTime.Now;

                string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(endereco)}&format=json&limit=1";

                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers.Add("user-agent", "ProjetoLogisticaCsharp/1.0");
                    string resposta = await client.DownloadStringTaskAsync(url);

                    if (resposta.Length > 2 && resposta.Contains("\"lat\""))
                    {
                        string latStr = ExtrairValorJson(resposta, "lat");
                        string lonStr = ExtrairValorJson(resposta, "lon");

                        double lat, lon;
                        if (double.TryParse(latStr, NumberStyles.Any, CultureInfo.InvariantCulture, out lat) &&
                            double.TryParse(lonStr, NumberStyles.Any, CultureInfo.InvariantCulture, out lon))
                        {
                            return new Ponto { Lat = lat, Lon = lon };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ultimoErro = ex.Message;
            }
            return null;
        }

        // ==========================================
        // DISTÂNCIA POR ROTA (OSRM) - retorna km, ou 0 se falhar
        // ==========================================
        private async Task<double> ObterDistanciaEmKm(Ponto origem, Ponto destino)
        {
            try
            {
                var ic = CultureInfo.InvariantCulture;
                string url = "https://router.project-osrm.org/route/v1/driving/"
                    + origem.Lon.ToString(ic) + "," + origem.Lat.ToString(ic) + ";"
                    + destino.Lon.ToString(ic) + "," + destino.Lat.ToString(ic)
                    + "?overview=false";

                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers.Add("user-agent", "ProjetoLogisticaCsharp/1.0");
                    string resposta = await client.DownloadStringTaskAsync(url);

                    string metrosStr = ExtrairNumeroJson(resposta, "distance");
                    double metros;
                    if (double.TryParse(metrosStr, NumberStyles.Any, ic, out metros) && metros > 0)
                    {
                        return Math.Round(metros / 1000.0, 2);
                    }
                    _ultimoErro = "O OSRM não retornou uma distância válida.";
                }
            }
            catch (Exception ex)
            {
                _ultimoErro = ex.Message;
            }
            return 0;
        }

        // ==========================================
        // BOTÃO CALCULAR FRETE
        // 1) acha as coordenadas  2) calcula o km  3) se deu certo, abre a rota no mapa
        // ==========================================
        private async void btnCalcularFrete_Click(object sender, EventArgs e)
        {
            btnCalcularFrete.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                kmCalculado = 0;
                txtKm.Text = "";
                _ultimoErro = "";

                // 1) Coordenadas
                Ponto origem = await ObterPontoComFallback(txtRuaO.Text, txtBairroO.Text, textCidadeO.Text, textUFO.Text);
                if (origem == null)
                {
                    MessageBox.Show("Não foi possível localizar a ORIGEM." +
                        (_ultimoErro != "" ? "\n" + _ultimoErro : ""), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Ponto destino = await ObterPontoComFallback(txtRuafim.Text, txtBairroFim.Text, textCidadeFim.Text, textUFFim.Text);
                if (destino == null)
                {
                    MessageBox.Show("Não foi possível localizar o DESTINO." +
                        (_ultimoErro != "" ? "\n" + _ultimoErro : ""), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2) Distância
                double km = await ObterDistanciaEmKm(origem, destino);
                if (km <= 0)
                {
                    MessageBox.Show("Não foi possível calcular a distância." +
                        (_ultimoErro != "" ? "\n" + _ultimoErro : ""), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                kmCalculado = km;
                txtKm.Text = km.ToString("0.00");

                // 3) Sem erros: mostra a rota no mapa usando as coordenadas
                var ic = CultureInfo.InvariantCulture;
                string urlMapa = "https://www.openstreetmap.org/directions?engine=fossgis_osrm_car&route="
                    + origem.Lat.ToString(ic) + "%2C" + origem.Lon.ToString(ic)
                    + "%3B"
                    + destino.Lat.ToString(ic) + "%2C" + destino.Lon.ToString(ic);

                if (webView21.CoreWebView2 == null)
                {
                    await webView21.EnsureCoreWebView2Async(null);
                }
                webView21.CoreWebView2.Navigate(urlMapa);

                string aviso = (origem.Aproximado || destino.Aproximado)
                    ? "\n\nAtenção: distância aproximada. Um dos endereços não foi encontrado com a rua, então usei o centro da cidade."
                    : "";
                MessageBox.Show($"Distância da rota: {txtKm.Text} km{aviso}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnCalcularFrete.Enabled = true;
            }
        }
    }
}