using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoLogistica
{
    public partial class Frm_orcamento : Form
    {
        public Frm_orcamento()
        {
            InitializeComponent();

            webBrowser1.ScriptErrorsSuppressed = true;
        }

        // ==========================================
        // BUSCA DE CEP - ORIGEM
        // ==========================================
        private void txtCepOrigem_Leave(object sender, EventArgs e)
        {
            string cep = txtCepOrigem.Text.Replace("-", "").Trim();

            if (cep.Length == 8)
            {
                try
                {
                    string url = $"https://viacep.com.br/ws/{cep}/json/";

                    using (WebClient client = new WebClient())
                    {
                        string resposta = client.DownloadString(url);

                        if (resposta.Contains("\"erro\":true") || resposta.Contains("\"erro\": true"))
                        {
                            MessageBox.Show("CEP de Origem não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string logradouro = ExtrairValorJson(resposta, "logradouro");
                        string bairro = ExtrairValorJson(resposta, "bairro");
                        string cidade = ExtrairValorJson(resposta, "localidade");
                        string uf = ExtrairValorJson(resposta, "uf");

                        // Preenchendo os seus campos exatos de Origem:
                        txtRuaO.Text = $"{logradouro} - {bairro}";
                        textCidadeO.Text = cidade;
                        textUFO.Text = uf;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar o CEP de Origem: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==========================================
        // BUSCA DE CEP - DESTINO
        // ==========================================
        private void txtCepDestino_Leave(object sender, EventArgs e)
        {
            string cep = txtCepDestino.Text.Replace("-", "").Trim();

            if (cep.Length == 8)
            {
                try
                {
                    string url = $"https://viacep.com.br/ws/{cep}/json/";

                    using (WebClient client = new WebClient())
                    {
                        string resposta = client.DownloadString(url);

                        if (resposta.Contains("\"erro\":true") || resposta.Contains("\"erro\": true"))
                        {
                            MessageBox.Show("CEP de Destino não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string logradouro = ExtrairValorJson(resposta, "logradouro");
                        string bairro = ExtrairValorJson(resposta, "bairro");
                        string cidade = ExtrairValorJson(resposta, "localidade");
                        string uf = ExtrairValorJson(resposta, "uf");

                        // Preenchendo os seus campos exatos de Destino:
                        txtRuafim.Text = $"{logradouro} - {bairro}";
                        textCidadeFim.Text = cidade;
                        textUFFim.Text = uf;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar o CEP de Destino: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==========================================
        // FUNÇÃO AUXILIAR PARA LER O JSON DO VIACEP
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


        // ==========================================
        // CÁLCULO DE DISTÂNCIA E ROTA EM KM
        // ==========================================
        private double ObterDistanciaEmKm(string origemEndereco, string destinoEndereco)
        {
            try
            {
                double latOrigem, lonOrigem;
                if (!ObterCoordenadas(origemEndereco, out latOrigem, out lonOrigem)) return 0;

                double latDestino, lonDestino;
                if (!ObterCoordenadas(destinoEndereco, out latDestino, out lonDestino)) return 0;

                string urlRota = $"http://router.project-osrm.org/route/v1/driving/{lonOrigem.ToString(System.Globalization.CultureInfo.InvariantCulture)},{latOrigem.ToString(System.Globalization.CultureInfo.InvariantCulture)};{lonDestino.ToString(System.Globalization.CultureInfo.InvariantCulture)},{latDestino.ToString(System.Globalization.CultureInfo.InvariantCulture)}?overview=false";

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2)");
                    string resposta = client.DownloadString(urlRota);

                    string distanciaMetrosStr = ExtrairValorJson(resposta, "distance");

                    if (double.TryParse(distanciaMetrosStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double distanciaMetros))
                    {
                        return Math.Round(distanciaMetros / 1000.0, 2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular a distância da rota: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return 0;
        }

        // Função auxiliar para buscar Latitude e Longitude
        private bool ObterCoordenadas(string endereco, out double latitude, out double longitude)
        {
            latitude = 0;
            longitude = 0;
            try
            {
                string urlGeo = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(endereco)}&format=json&limit=1";

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("user-agent", "ProjetoLogisticaCsharp/1.0");
                    string resposta = client.DownloadString(urlGeo);

                    if (resposta.Length > 2 && resposta.Contains("lat"))
                    {
                        string latStr = ExtrairValorJson(resposta.TrimStart('[').TrimEnd(']'), "lat");
                        string lonStr = ExtrairValorJson(resposta.TrimStart('[').TrimEnd(']'), "lon");

                        if (double.TryParse(latStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out latitude) &&
                            double.TryParse(lonStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out longitude))
                        {
                            return true;
                        }
                    }
                }
            }
            catch { }
            return false;
        }






        private void btnCalcularFrete_Click(object sender, EventArgs e)
        {
            // Monta o endereço completo de origem e destino usando os campos que já foram preenchidos pelo CEP
            string origemCompleta = $"{txtRuaO.Text}, {textCidadeO.Text} - {textUFO.Text}";
            string destinoCompleta = $"{txtRuafim.Text}, {textCidadeFim.Text} - {textUFFim.Text}";

            // Chama a função que calcula os quilômetros
            double kmTotal = ObterDistanciaEmKm(origemCompleta, destinoCompleta);

            if (kmTotal > 0)
            {
                MessageBox.Show($"A distância calculada da rota é de: {kmTotal} km", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Exemplo: se você tiver um campo txtKm no form, pode preencher ele:
                // txtKm.Text = kmTotal.ToString();

                // A partir daqui, você multiplica o km pelo valor do frete por km do caminhão!
            }
            else
            {
                MessageBox.Show("Não foi possível calcular a distância exata para os endereços informados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                
                
                string origem = $"{textCidadeO.Text} - {textUFO.Text}";
                string destino = $"{textCidadeFim.Text} - {textUFFim.Text}";

                // Abre o OpenStreetMap focando na rota entre as cidades
                string urlMapa = $"https://www.openstreetmap.org/directions?engine=fossgis_osrm_car&route={Uri.EscapeDataString(origem)}%3B{Uri.EscapeDataString(destino)}";

                webBrowser1.Navigate(urlMapa);
            }
        }

        private void Frm_orcamento_Load(object sender, EventArgs e)
        {

        }
    }
}
