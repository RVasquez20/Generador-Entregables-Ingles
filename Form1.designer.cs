using System.Drawing;
using System.Windows.Forms;

namespace Generador
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            GenerarToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            panel2 = new Panel();
            txt_Imagenes = new TextBox();
            Imagenes = new Panel();
            cb_Nivel = new ComboBox();
            label1 = new Label();
            cb_TipoEntrega = new ComboBox();
            lbl_Proyecto = new Label();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ControlLight;
            menuStrip1.Dock = DockStyle.Left;
            menuStrip1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            menuStrip1.GripMargin = new Padding(2, 2, 0, 5);
            menuStrip1.Items.AddRange(new ToolStripItem[] { GenerarToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(126, 450);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // GenerarToolStripMenuItem
            // 
            GenerarToolStripMenuItem.Name = "GenerarToolStripMenuItem";
            GenerarToolStripMenuItem.Size = new Size(113, 19);
            GenerarToolStripMenuItem.Text = "Generar PDF";
            GenerarToolStripMenuItem.Click += GenerarToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(Imagenes);
            panel1.Controls.Add(cb_Nivel);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(cb_TipoEntrega);
            panel1.Controls.Add(lbl_Proyecto);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(126, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(674, 450);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = SystemColors.ControlLightLight;
            panel2.Controls.Add(txt_Imagenes);
            panel2.Location = new Point(339, 90);
            panel2.Name = "panel2";
            panel2.Size = new Size(335, 360);
            panel2.TabIndex = 11;
            // 
            // txt_Imagenes
            // 
            txt_Imagenes.Dock = DockStyle.Fill;
            txt_Imagenes.Enabled = false;
            txt_Imagenes.Location = new Point(0, 0);
            txt_Imagenes.Multiline = true;
            txt_Imagenes.Name = "txt_Imagenes";
            txt_Imagenes.Size = new Size(335, 360);
            txt_Imagenes.TabIndex = 0;
            // 
            // Imagenes
            // 
            Imagenes.AllowDrop = true;
            Imagenes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Imagenes.BorderStyle = BorderStyle.FixedSingle;
            Imagenes.Location = new Point(0, 90);
            Imagenes.Name = "Imagenes";
            Imagenes.Size = new Size(303, 360);
            Imagenes.TabIndex = 10;
            Imagenes.DragDrop += Imagenes_DragDrop;
            Imagenes.DragEnter += Imagenes_DragEnter;
            Imagenes.DragLeave += Imagenes_DragLeave;
            // 
            // cb_Nivel
            // 
            cb_Nivel.FormattingEnabled = true;
            cb_Nivel.Location = new Point(183, 61);
            cb_Nivel.Name = "cb_Nivel";
            cb_Nivel.Size = new Size(360, 23);
            cb_Nivel.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(104, 64);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 8;
            label1.Text = "Nivel";
            // 
            // cb_TipoEntrega
            // 
            cb_TipoEntrega.FormattingEnabled = true;
            cb_TipoEntrega.Location = new Point(183, 20);
            cb_TipoEntrega.Name = "cb_TipoEntrega";
            cb_TipoEntrega.Size = new Size(360, 23);
            cb_TipoEntrega.TabIndex = 7;
            // 
            // lbl_Proyecto
            // 
            lbl_Proyecto.AutoSize = true;
            lbl_Proyecto.Location = new Point(104, 23);
            lbl_Proyecto.Name = "lbl_Proyecto";
            lbl_Proyecto.Size = new Size(73, 15);
            lbl_Proyecto.TabIndex = 6;
            lbl_Proyecto.Text = "Tipo Entrega";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            Name = "Form1";
            Text = "Generador Tareas Ingles";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem GenerarToolStripMenuItem;
        private Panel panel1;
        private ComboBox cb_Nivel;
        private Label label1;
        private ComboBox cb_TipoEntrega;
        private Label lbl_Proyecto;
        private Panel Imagenes;
        private Panel panel2;
        private TextBox txt_Imagenes;
    }
}