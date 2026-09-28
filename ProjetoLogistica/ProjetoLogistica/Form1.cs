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
    public partial class Frm_inicial : Form
    {
        public Frm_inicial()
        {
            InitializeComponent();
        }

        private void Pic_Frm_escolhacaminhao_Click(object sender, EventArgs e)
        {
            Frm_tiposdecaminhao Chametelatipodecaminhao = new Frm_tiposdecaminhao();
            Chametelatipodecaminhao.Show();
        }
    }
}
