﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema_gestao_estudantes
{
    public partial class FormListaDeEstudantes : Form
    {
        public FormListaDeEstudantes()
        {
            InitializeComponent();
        }

        Estudante estudante = new Estudante();

        private void FormListaDeEstudantes_Load(object sender, EventArgs e)
        {
            // Preenche o "DataGridView" com os dados dos estudantes.
            MySqlCommand comando = new MySqlCommand("SELECT * FROM `estudantes`");
            dataGridViewLista.ReadOnly = true;
            DataGridViewImageColumn colunaDeFotos = new DataGridViewImageColumn();
            dataGridViewLista.RowTemplate.Height = 80;
            dataGridViewLista.DataSource = estudante.getEstudantes(comando);
            colunaDeFotos = (DataGridViewImageColumn)dataGridViewLista.Columns[7];
            colunaDeFotos.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridViewLista.AllowUserToAddRows = false;
        }

        private void dataGridViewLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewLista_DoubleClick(object sender, EventArgs e)
        {
            // Abre o estudante selecionado.

            AtualizarDeletarEstudante atualizarDeletarEstudante = new AtualizarDeletarEstudante();
            atualizarDeletarEstudante.textBoxID.Text = dataGridViewLista.CurrentRow.Cells[0].Value.ToString();
            atualizarDeletarEstudante.textBoxNOME.Text = dataGridViewLista.CurrentRow.Cells[1].Value.ToString();
            atualizarDeletarEstudante.textBoxSOBRENOME.Text = dataGridViewLista.CurrentRow.Cells[2].Value.ToString();
            atualizarDeletarEstudante.dateTimePickerNascimento.Value = (DateTime) dataGridViewLista.CurrentRow.Cells[3].Value;

            if (dataGridViewLista.CurrentRow.Cells[4].Value.ToString() == "Feminino")
            {
                atualizarDeletarEstudante.raddioButtonFeminino.Checked = true;
            }
          else
            {
                atualizarDeletarEstudante.raddioButtonMasculino.Checked = true;
            }
            atualizarDeletarEstudante.textBoxTel.Text = dataGridViewLista.CurrentRow.Cells[5].Value.ToString();
            atualizarDeletarEstudante.textBoxEND.Text = dataGridViewLista.CurrentRow.Cells[6].Value.ToString();

            byte[] fotoDaLista;
            fotoDaLista = (byte[])dataGridViewLista.CurrentRow.Cells[7].Value;
            MemoryStream fotoDoEstudante = new MemoryStream(fotoDaLista);
            atualizarDeletarEstudante.pictureBoxFOTO.Image = Image.FromStream (fotoDoEstudante);
            atualizarDeletarEstudante.Show();
        }

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            // Atualizar a lista de estudantes.
        }

        private void FormListaDeEstudantes_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
