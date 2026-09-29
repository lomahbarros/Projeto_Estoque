using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoLogistica
{
    public partial class Frm_tiposdecaminhao : Form
    {
        public Frm_tiposdecaminhao()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
            // 1. Limpa qualquer coluna ou linha anterior para evitar duplicação
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // 2. Adiciona as Colunas
            dataGridView1.Columns.Add("colTipoCaminhao", "Tipo de Caminhão / Baú");
            dataGridView1.Columns.Add("colComprimento", "Comprimento (m)");
            dataGridView1.Columns.Add("colAltura", "Altura (m)");
            dataGridView1.Columns.Add("colLargura", "Largura (m)");
            dataGridView1.Columns.Add("colVolume", "Volume Aproximado");
            dataGridView1.Columns.Add("colCapacidadeCarga", "Capacidade de Carga (t)");
            dataGridView1.Columns.Add("colUsoTipico", "Uso Típico");

            // 3. Adiciona as Linhas com os Dados da Imagem
            dataGridView1.Rows.Add(
                "VUC (Veículo Urbano de Carga)",
                "5,0 – 6,0",
                "2,2",
                "2,1",
                "10 – 12",
                "3,0 – 3,5",
                "Entregues urbanas, e-commerce"
            );

            dataGridView1.Rows.Add(
                "Caminhão 3/4",
                "6,0 – 7,0",
                "2,4",
                "2,3",
                "18 – 22",
                "4,0 – 5,0",
                "Distribuição regional leve"
            );

            dataGridView1.Rows.Add(
                "Caminhão Toco (Simples)",
                "7,0 – 8,0",
                "2,6",
                "2,4",
                "25 – 30",
                "6,0 – 8,0",
                "Carga seca, alimentos, varejo"
            );

            dataGridView1.Rows.Add(
                "Caminhão Trucado",
                "8,0 – 9,0",
                "2,7",
                "2,4",
                "35 – 40",
                "12 – 14",
                "Transporte regional e pesado"
            );

            dataGridView1.Rows.Add(
                "Carreta Baú Simples (3 eixos)",
                "14,0 – 15,0",
                "2,8",
                "2,6",
                "75 – 85",
                "27 – 33",
                "Transporte interestadual"
            );

            // 4. Ajustes visuais recomendados para a tabela
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Preenche o espaço da tela
            dataGridView1.AllowUserToAddRows = false; // Remove a linha vazia no final
            dataGridView1.ReadOnly = true; // Impede que o usuário edite os dados clicando neles
        }

        private void Frm_tiposdecaminhao_Load(object sender, EventArgs e)
        
            
        {
            // 1. Limpa qualquer coluna ou linha anterior para evitar duplicação
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // 2. Adiciona as Colunas
            dataGridView1.Columns.Add("colTipoCaminhao", "Tipo de Caminhão / Baú");
            dataGridView1.Columns.Add("colComprimento", "Comprimento (m)");
            dataGridView1.Columns.Add("colAltura", "Altura (m)");
            dataGridView1.Columns.Add("colLargura", "Largura (m)");
            dataGridView1.Columns.Add("colVolume", "Volume Aproximado");
            dataGridView1.Columns.Add("colCapacidadeCarga", "Capacidade de Carga (t)");
            dataGridView1.Columns.Add("colUsoTipico", "Uso Típico");

            // 3. Adiciona as Linhas com os Dados da Imagem
            dataGridView1.Rows.Add(
                "VUC (Veículo Urbano de Carga)",
                "5,0 – 6,0",
                "2,2",
                "2,1",
                "10 – 12",
                "3,0 – 3,5",
                "Entregues urbanas, e-commerce"
            );

            dataGridView1.Rows.Add(
                "Caminhão 3/4",
                "6,0 – 7,0",
                "2,4",
                "2,3",
                "18 – 22",
                "4,0 – 5,0",
                "Distribuição regional leve"
            );

            dataGridView1.Rows.Add(
                "Caminhão Toco (Simples)",
                "7,0 – 8,0",
                "2,6",
                "2,4",
                "25 – 30",
                "6,0 – 8,0",
                "Carga seca, alimentos, varejo"
            );

            dataGridView1.Rows.Add(
                "Caminhão Trucado",
                "8,0 – 9,0",
                "2,7",
                "2,4",
                "35 – 40",
                "12 – 14",
                "Transporte regional e pesado"
            );

            
            

            // 4. Ajustes visuais recomendados para a tabela
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Preenche o espaço da tela
            dataGridView1.AllowUserToAddRows = false; // Remove a linha vazia no final
            dataGridView1.ReadOnly = true; // Impede que o usuário edite os dados clicando neles
        }

        private void Btn_VUC_Click(object sender, EventArgs e)
        {
            Pic_imagens.Image = Properties.Resources.VUC__Veículo_Urbano_de_Carga_;
            Pic_ft01.Image = Properties.Resources.b02;
            Pic_ft02.Image = Properties.Resources.b03;
            Pic_ft03.Image = Properties.Resources.b0222;

        }

        private void Btn_3q_Click(object sender, EventArgs e)
        {
            Pic_imagens.Image = Properties.Resources.Caminhão_3_4;
            Pic_ft01.Image = Properties.Resources.motorista01;
            Pic_ft02.Image = Properties.Resources.motorista02;
            Pic_ft03.Image = Properties.Resources.motorista03;

        }

        private void Btn_toco_Click(object sender, EventArgs e)
        {
            Pic_imagens.Image = Properties.Resources.TOCO;
            Pic_ft01.Image = Properties.Resources.motorista01;
            Pic_ft02.Image = Properties.Resources.motorista02;
            Pic_ft03.Image = Properties.Resources.motorista04;
        }

        private void Btn_truck_Click(object sender, EventArgs e)
        {
            Pic_imagens.Image = Properties.Resources.truck;
            Pic_ft01.Image = Properties.Resources.motorista05;
            Pic_ft02.Image = Properties.Resources.motorista06;
            Pic_ft03.Image = Properties.Resources.motorista04;
        }
    }
    }
    

