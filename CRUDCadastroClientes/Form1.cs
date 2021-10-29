using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CRUDCadastroClientes
{
    public partial class CadastroClientesCRUD : Form
    {
        public CadastroClientesCRUD()
        {
            InitializeComponent();
        }
        OleDbConnection con = null;
        OleDbDataAdapter da = null;
        private void CadastroClientesCRUD_Load(object sender, EventArgs e)
        {
            try
            {
                con = new OleDbConnection(Conexao.Local());
                da = new OleDbDataAdapter("select * from Clientes", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Clientes");
                BindingSource bs = new BindingSource();
                bs.DataSource = ds.Tables["Clientes"];
                txtID.DataBindings.Add("text", bs, "ID");
                txtNome.DataBindings.Add("text", bs, "Nome");
                txtEmail.DataBindings.Add("text", bs, "Email");
                txtCPF.DataBindings.Add("text", bs, "CPF");
                txtID.Enabled = false;
                // Transforma todos os dados que forem inseridos em seu form em forma de Data para serem inseridos na table do seu Database.
            }
            catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Deseja sair da aplicação?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(dr == DialogResult.Yes)
            {
                Application.Exit();
            }
            // Se você clicar em sim sairá da aplicação.
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtNome.Text == "" || txtEmail.Text == "" || txtCPF.Text == "")
                {
                    MessageBox.Show("Informe os dados necessários", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                    // Caso algum campo não esteja preenchido, uma mensagem alertando o usuário disso será mostrada.
                }
                Clientes cliente = new Clientes();
                cliente.Nome = txtNome.Text;
                cliente.Email = txtEmail.Text;
                cliente.CPF = txtCPF.Text;
                cliente.AdicionarNovo();
                // Preenche os dados com os respectivos campos e inclui estes nos campos da tabela.
                MessageBox.Show("Cliente incluso na base de dados.");
                txtNome.Clear();
                txtEmail.Clear();
                txtCPF.Clear();
                txtNome.Focus();
                // Limpa os dados e foca no nome.
                Clientes c = new Clientes();
                DataSet ds = c.Att();
                dgvExibirDados.DataSource = ds.Tables[0];
                ExibirDados();
                // Exibe os dados no DataGridView.
            }
            catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        private void ExibirDados()
        {
            try
            {
                Clientes cliente = new Clientes();
                DataSet ds = cliente.Att();
                dgvExibirDados.DataSource = ds.Tables[0];
                // Atualiza o cliente usando o select e mostra na DataGridView.
            }
            catch
            {
                throw;
            }
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            if(txtProcurar.Text == "")
            {
                MessageBox.Show("Você não inseriu o id do cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Caso o campo  de procurar não esteja preenchido, uma mensagem alertando o usuário disso será mostrada.
            }
            else
            {
                int id = Convert.ToInt32(txtProcurar.Text);
                DataSet ds = Clientes.Procurar(id);
                // Usa o select para procurar um id correspondente ao informado na textbox.
                if(ds.Tables[0].Rows.Count > 0)
                {
                    dgvExibirDados.DataSource = ds.Tables["DGV1"];
                    dgvExibirDados.Refresh();
                    dgvExibirDados.Update();
                    // Exibe os dados somente do id correspondente.
                }
                else
                {
                    MessageBox.Show("Dados não localizados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ExibirDados();
                }
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Deseja excluir o cliente da base de dados?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(dr == DialogResult.Yes)
                // Se você clicar em sim ele irá executar as ações abaixo.
                {
                    Clientes cliente = new Clientes();
                    int id = Convert.ToInt32(txtID.Text);
                    cliente.Nome = txtNome.Text;
                    cliente.Email = txtEmail.Text;
                    cliente.CPF = txtCPF.Text;
                    cliente.Deletar(id);
                    // Deletará do banco de dados o cliente cuja ID corresponda a selecionada no DataGridView.
                    MessageBox.Show("Cliente excluído");
                    ExibirDados();
                }
            }
            catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtEmail.Text == "" || txtCPF.Text == "")
            {
                MessageBox.Show("Informe os dados necessários", "Atualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                // Caso algum campo não esteja preenchido, uma mensagem alertando o usuário disso será mostrada.
            }
            Clientes cliente = new Clientes();
            int id = Convert.ToInt32(txtID.Text);
            cliente.Nome = txtNome.Text;
            cliente.Email = txtEmail.Text;
            cliente.CPF = txtCPF.Text;

            cliente.Atualizar(id);
            // Atualiza os dados para strings correpondes as mudanças que você realize nas textboxs.
            MessageBox.Show("Dados atualizados");
            ExibirDados();

        }

        private void btnExibir_Click(object sender, EventArgs e)
        {
            ExibirDados();
            // Exibe os dados.
        }

        private void dgvExibirDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvExibirDados.Rows[e.RowIndex];
                txtID.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                txtEmail.Text = row.Cells[2].Value.ToString();
                txtCPF.Text = row.Cells[3].Value.ToString();
                // Exibe no seu DataGridView os dados nas colunas correspondentes aos números.
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNome.Text, @"^[a-zA-Z\u00C0-\u00FF ]*$"))
            {
                txtNome.Clear();
            }
            // Se o texto for diferente dos caracteres aceitos a textbox será limpa.
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9@.]*$"))
            {
                txtEmail.Clear();
            }
            // Se o texto for diferente dos caracteres aceitos a textbox será limpa.
        }

        private void txtCPF_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtCPF.Text, @"^[0-9]*$"))
            {
                txtCPF.Clear();
            }
            // Se o texto for diferente dos caracteres aceitos a textbox será limpa.
        }

        private void CadastroClientesCRUD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar.CompareTo((char)Keys.Return)) == 0)
            {
                e.Handled = true;
                SendKeys.Send("TAB");
            }
            // Se tab for pressionado ou shift tab levará ao próximo campo.
        }
    }
}
