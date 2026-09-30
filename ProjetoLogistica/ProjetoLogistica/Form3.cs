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
                        // Garante o UTF-8 para evitar caracteres estranhos e acentos corrompidos
                        client.Encoding = System.Text.Encoding.UTF8;

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

                        // Preenchendo os campos específicos de ORIGEM:
                        txtRuaO.Text = logradouro;
                        txtBairroO.Text = bairro;   // <--- Caixa específica do bairro de origem
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
                        // GARANTE O UTF-8 PARA CORRIGIR OS ACENTOS
                        client.Encoding = System.Text.Encoding.UTF8;

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

                        // Preenchendo os campos de Destino de forma separada e limpa:
                        txtRuafim.Text = logradouro;
                        txtBairroFim.Text = bairro;   // <--- Atribui o bairro à nova caixinha (confirme se o nome do seu componente é este)
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
                if (!ObterCoordenadas(origemEndereco, out latOrigem, out lonOrigem))
                {
                    MessageBox.Show("Falha ao buscar coordenadas da ORIGEM:\n" + origemEndereco, "Diagnóstico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 0;
                }

                double latDestino, lonDestino;
                if (!ObterCoordenadas(destinoEndereco, out latDestino, out lonDestino))
                {
                    MessageBox.Show("Falha ao buscar coordenadas do DESTINO:\n" + destinoEndereco, "Diagnóstico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 0;
                }

                string urlRota = $"https://router.project-osrm.org/route/v1/driving/{lonOrigem.ToString(System.Globalization.CultureInfo.InvariantCulture)},{latOrigem.ToString(System.Globalization.CultureInfo.InvariantCulture)};{lonDestino.ToString(System.Globalization.CultureInfo.InvariantCulture)},{latDestino.ToString(System.Globalization.CultureInfo.InvariantCulture)}?overview=false";

                using (WebClient client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers.Add("user-agent", "ProjetoLogisticaCsharp/1.0");
                    string resposta = client.DownloadString(urlRota);

                    string distanciaMetrosStr = ExtrairValorJson(resposta, "distance");

                    if (double.TryParse(distanciaMetrosStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double distanciaMetros))
                    {
                        return Math.Round(distanciaMetros / 1000.0, 2);
                    }
                    else
                    {
                        MessageBox.Show("A API de rotas (OSRM) não retornou um valor numérico válido.", "Diagnóstico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceção capturada no cálculo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return 0;
        }

        // Função corrigida para buscar Latitude e Longitude com suporte a Acentos (UTF-8)
        private bool ObterCoordenadas(string endereco, out double latitude, out double longitude)
        {
            latitude = 0;
            longitude = 0;
            try
            {
                string urlGeo = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(endereco)}&format=json&limit=1";

                using (WebClient client = new WebClient())
                {
                    // Força o uso de UTF-8 para aceitar acentos corretamente (Ç, ã, é, etc.)
                    client.Encoding = System.Text.Encoding.UTF8;

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
            catch (Exception ex)
            {
                // Opcional: descomente a linha abaixo se quiser ver se deu algum erro de rede específico
                // MessageBox.Show("Erro em ObterCoordenadas: " + ex.Message);
            }
            return false;
        }






        private void btnCalcularFrete_Click(object sender, EventArgs e)
        {
            // Monta o endereço completo de origem e destino incluindo os campos de bairro
            string origemCompleta = $"{txtRuaO.Text}, {txtBairroO.Text}, {textCidadeO.Text} - {textUFO.Text}";
            string destinoCompleta = $"{txtRuafim.Text}, {txtBairroFim.Text}, {textCidadeFim.Text} - {textUFFim.Text}";

            // Chama a função que calcula os quilómetros
            double kmTotal = ObterDistanciaEmKm(origemCompleta, destinoCompleta);

            if (kmTotal > 0)
            {
                MessageBox.Show($"A distância calculada da rota é de: {kmTotal} km", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ATIVADO: Preenche o campo txtKm no formulário corretamente
                txtKm.Text = kmTotal.ToString();
            }
            else
            {
                MessageBox.Show("Não foi possível calcular a distância exata para os endereços informados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                string origem = $"{textCidadeO.Text} - {textUFO.Text}";
                string destino = $"{textCidadeFim.Text} - {textUFFim.Text}";

                // Abre o OpenStreetMap focando na rota entre as cidades como contingência
                string urlMapa = $"https://www.openstreetmap.org/directions?engine=fossgis_osrm_car&route={Uri.EscapeDataString(origem)}%3B{Uri.EscapeDataString(destino)}";

                webBrowser1.Navigate(urlMapa);
            }
        }

        private void Frm_orcamento_Load(object sender, EventArgs e)
        {

        }
    }
}
