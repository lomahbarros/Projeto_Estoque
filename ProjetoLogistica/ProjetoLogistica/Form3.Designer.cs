namespace ProjetoLogistica
{
    partial class Frm_orcamento
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Lbl_titulo_telaorcamento = new System.Windows.Forms.Label();
            this.txtRuaOrigem = new System.Windows.Forms.Label();
            this.txtCidadeOrigem = new System.Windows.Forms.Label();
            this.textCidadeO = new System.Windows.Forms.TextBox();
            this.txtUfOrigem = new System.Windows.Forms.Label();
            this.textUFO = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCepDestino = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCepOrigem = new System.Windows.Forms.MaskedTextBox();
            this.txtRuaO = new System.Windows.Forms.TextBox();
            this.txtRuafim = new System.Windows.Forms.TextBox();
            this.textUFFim = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textCidadeFim = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCalcularFrete = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBairroO = new System.Windows.Forms.TextBox();
            this.txtBairroFim = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKm = new System.Windows.Forms.Label();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.SuspendLayout();
            // 
            // Lbl_titulo_telaorcamento
            // 
            this.Lbl_titulo_telaorcamento.AutoSize = true;
            this.Lbl_titulo_telaorcamento.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_titulo_telaorcamento.ForeColor = System.Drawing.Color.Black;
            this.Lbl_titulo_telaorcamento.Location = new System.Drawing.Point(336, 9);
            this.Lbl_titulo_telaorcamento.Name = "Lbl_titulo_telaorcamento";
            this.Lbl_titulo_telaorcamento.Size = new System.Drawing.Size(222, 28);
            this.Lbl_titulo_telaorcamento.TabIndex = 0;
            this.Lbl_titulo_telaorcamento.Text = "Orçamento do frete";
            // 
            // txtRuaOrigem
            // 
            this.txtRuaOrigem.AutoSize = true;
            this.txtRuaOrigem.Location = new System.Drawing.Point(19, 111);
            this.txtRuaOrigem.Name = "txtRuaOrigem";
            this.txtRuaOrigem.Size = new System.Drawing.Size(92, 13);
            this.txtRuaOrigem.TabIndex = 1;
            this.txtRuaOrigem.Text = "Rua / Logradouro";
            // 
            // txtCidadeOrigem
            // 
            this.txtCidadeOrigem.AutoSize = true;
            this.txtCidadeOrigem.Location = new System.Drawing.Point(258, 115);
            this.txtCidadeOrigem.Name = "txtCidadeOrigem";
            this.txtCidadeOrigem.Size = new System.Drawing.Size(40, 13);
            this.txtCidadeOrigem.TabIndex = 3;
            this.txtCidadeOrigem.Text = "Cidade";
            // 
            // textCidadeO
            // 
            this.textCidadeO.Location = new System.Drawing.Point(318, 112);
            this.textCidadeO.Name = "textCidadeO";
            this.textCidadeO.Size = new System.Drawing.Size(100, 20);
            this.textCidadeO.TabIndex = 4;
            // 
            // txtUfOrigem
            // 
            this.txtUfOrigem.AutoSize = true;
            this.txtUfOrigem.Location = new System.Drawing.Point(624, 112);
            this.txtUfOrigem.Name = "txtUfOrigem";
            this.txtUfOrigem.Size = new System.Drawing.Size(40, 13);
            this.txtUfOrigem.TabIndex = 5;
            this.txtUfOrigem.Text = "Estado";
            // 
            // textUFO
            // 
            this.textUFO.Location = new System.Drawing.Point(679, 108);
            this.textUFO.Name = "textUFO";
            this.textUFO.Size = new System.Drawing.Size(100, 20);
            this.textUFO.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "CEP de Origem";
            // 
            // txtCepDestino
            // 
            this.txtCepDestino.Location = new System.Drawing.Point(101, 218);
            this.txtCepDestino.Mask = "00000-000";
            this.txtCepDestino.Name = "txtCepDestino";
            this.txtCepDestino.Size = new System.Drawing.Size(100, 20);
            this.txtCepDestino.TabIndex = 8;
            this.txtCepDestino.Leave += new System.EventHandler(this.txtCepDestino_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "CEP de Destivo";
            // 
            // txtCepOrigem
            // 
            this.txtCepOrigem.Location = new System.Drawing.Point(128, 70);
            this.txtCepOrigem.Mask = "00000-000";
            this.txtCepOrigem.Name = "txtCepOrigem";
            this.txtCepOrigem.Size = new System.Drawing.Size(100, 20);
            this.txtCepOrigem.TabIndex = 10;
            this.txtCepOrigem.Leave += new System.EventHandler(this.txtCepOrigem_Leave);
            // 
            // txtRuaO
            // 
            this.txtRuaO.Location = new System.Drawing.Point(138, 105);
            this.txtRuaO.Name = "txtRuaO";
            this.txtRuaO.Size = new System.Drawing.Size(100, 20);
            this.txtRuaO.TabIndex = 11;
            // 
            // txtRuafim
            // 
            this.txtRuafim.Location = new System.Drawing.Point(142, 265);
            this.txtRuafim.Name = "txtRuafim";
            this.txtRuafim.Size = new System.Drawing.Size(100, 20);
            this.txtRuafim.TabIndex = 17;
            // 
            // textUFFim
            // 
            this.textUFFim.Location = new System.Drawing.Point(679, 271);
            this.textUFFim.Name = "textUFFim";
            this.textUFFim.Size = new System.Drawing.Size(100, 20);
            this.textUFFim.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(624, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Estado";
            // 
            // textCidadeFim
            // 
            this.textCidadeFim.Location = new System.Drawing.Point(322, 272);
            this.textCidadeFim.Name = "textCidadeFim";
            this.textCidadeFim.Size = new System.Drawing.Size(100, 20);
            this.textCidadeFim.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Cidade";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Rua / Logradouro";
            // 
            // btnCalcularFrete
            // 
            this.btnCalcularFrete.Location = new System.Drawing.Point(853, 422);
            this.btnCalcularFrete.Name = "btnCalcularFrete";
            this.btnCalcularFrete.Size = new System.Drawing.Size(75, 23);
            this.btnCalcularFrete.TabIndex = 18;
            this.btnCalcularFrete.Text = "Mostrar Rota";
            this.btnCalcularFrete.UseVisualStyleBackColor = true;
            this.btnCalcularFrete.Click += new System.EventHandler(this.btnCalcularFrete_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(436, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Bairro";
            // 
            // txtBairroO
            // 
            this.txtBairroO.Location = new System.Drawing.Point(489, 104);
            this.txtBairroO.Name = "txtBairroO";
            this.txtBairroO.Size = new System.Drawing.Size(100, 20);
            this.txtBairroO.TabIndex = 21;
            // 
            // txtBairroFim
            // 
            this.txtBairroFim.Location = new System.Drawing.Point(506, 272);
            this.txtBairroFim.Name = "txtBairroFim";
            this.txtBairroFim.Size = new System.Drawing.Size(100, 20);
            this.txtBairroFim.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(453, 279);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Bairro";
            // 
            // txtKm
            // 
            this.txtKm.AutoSize = true;
            this.txtKm.Location = new System.Drawing.Point(789, 427);
            this.txtKm.Name = "txtKm";
            this.txtKm.Size = new System.Drawing.Size(33, 13);
            this.txtKm.TabIndex = 24;
            this.txtKm.Text = "txtKm";
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Location = new System.Drawing.Point(-6, 451);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(983, 205);
            this.webView21.TabIndex = 25;
            this.webView21.ZoomFactor = 1D;
            // 
            // Frm_orcamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ProjetoLogistica.Properties.Resources.backgr;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(964, 761);
            this.Controls.Add(this.webView21);
            this.Controls.Add(this.txtKm);
            this.Controls.Add(this.txtBairroFim);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBairroO);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCalcularFrete);
            this.Controls.Add(this.txtRuafim);
            this.Controls.Add(this.textUFFim);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textCidadeFim);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRuaO);
            this.Controls.Add(this.txtCepOrigem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCepDestino);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textUFO);
            this.Controls.Add(this.txtUfOrigem);
            this.Controls.Add(this.textCidadeO);
            this.Controls.Add(this.txtCidadeOrigem);
            this.Controls.Add(this.txtRuaOrigem);
            this.Controls.Add(this.Lbl_titulo_telaorcamento);
            this.Name = "Frm_orcamento";
            this.Text = "Orçamento";
            this.Load += new System.EventHandler(this.Frm_orcamento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_titulo_telaorcamento;
        private System.Windows.Forms.Label txtRuaOrigem;
        private System.Windows.Forms.Label txtCidadeOrigem;
        private System.Windows.Forms.TextBox textCidadeO;
        private System.Windows.Forms.Label txtUfOrigem;
        private System.Windows.Forms.TextBox textUFO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox txtCepDestino;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtCepOrigem;
        private System.Windows.Forms.TextBox txtRuaO;
        private System.Windows.Forms.TextBox txtRuafim;
        private System.Windows.Forms.TextBox textUFFim;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textCidadeFim;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCalcularFrete;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBairroO;
        private System.Windows.Forms.TextBox txtBairroFim;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label txtKm;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
    }
}