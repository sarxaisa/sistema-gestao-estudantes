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
    public partial class AtualizarDeletarEstudante : Form
    {
        public AtualizarDeletarEstudante()
        {
            InitializeComponent();
        }

        private void buttonEnviarFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrirArquivo = new OpenFileDialog();
            abrirArquivo.Filter =
                "Seleciona a Foto(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (abrirArquivo.ShowDialog() == DialogResult.OK)
            {
                pictureBoxFOTO.Image = Image.FromFile(abrirArquivo.FileName);
            }
        }

        bool Verificar()
        {
            if ((textBoxNOME.Text.Trim() == "") ||
                (textBoxSOBRENOME.Text.Trim() == "") ||
                (textBoxTel.Text.Trim() == "") ||
                (textBoxEND.Text.Trim() == "") ||
                (pictureBoxFOTO.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void buttonConfirmar_Click(object sender, EventArgs e)
        {
            // Atualiza as informações do estudante.
            Estudante estudante = new Estudante();
            int id = Convert.ToInt32(((TextBox)sender).Text);
            string nome = textBoxNOME.Text;
            string sobrenome = textBoxSOBRENOME.Text;
            DateTime nascimento = dateTimePickerNascimento.Value;
            string telefone = textBoxTel.Text;
            string endereco = textBoxEND.Text;
            string genero = "Feminino";

            if (raddioButtonMasculino.Checked)
            {
                genero = "Masculino";
            }

            // A foto do estudante em formato binário.
            MemoryStream foto = new MemoryStream();

            // Verifica se o estudante é maior de 10 anos.
            int anoDeNascimento = dateTimePickerNascimento.Value.Year;
            // Pega o ano no qual estamos.
            int anoAtual = DateTime.Now.Year;
            if (
                (anoAtual - anoDeNascimento) < 10 ||
                (anoAtual - anoDeNascimento) > 100
                )
            {
                MessageBox.Show("A idade precisa ser entre 10 e 100 anos.",
                    "Idade Inválida",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Verificar())
            {
                pictureBoxFOTO.Image.Save(foto, pictureBoxFOTO.Image.RawFormat);
                if (estudante.atualizarEstudante(id, nome, sobrenome, nascimento,
                    telefone, genero, endereco, foto))
                {
                    MessageBox.Show("Informações atualizadas", "Sucesso!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro", "Inserir Estudante",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Campos não preenchidos",
                    "Inserir Estudante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
