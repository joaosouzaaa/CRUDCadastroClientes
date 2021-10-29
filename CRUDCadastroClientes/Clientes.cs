using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace CRUDCadastroClientes
{
    class Clientes
    {
        public int ID;
        public string Nome;
        public string Email;
        public string CPF;
        // Construtores, informações aonde serão armazenados os dados correspondentes.

        public DataSet Att()
        {
            OleDbConnection con = new OleDbConnection(Conexao.Local());
            con.Open();
            // Faz a conexão com o banco de dados e a abre.
            OleDbDataAdapter da = new OleDbDataAdapter("Select ID as ID, Nome as Nome, Email as Email, CPF as CPF From Clientes", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            // Adapta seus dados e dá um Select no seu banco de dados, atualizando-o.
            return ds;
        }
        public void AdicionarNovo()
        {
            OleDbConnection con = new OleDbConnection(Conexao.Local());
            con.Open();
            OleDbCommand cmd = new OleDbCommand("Insert Into Clientes(Nome, Email, CPF) Values('" + Nome + "','" + Email + "','" + CPF + "')", con);
            cmd.ExecuteNonQuery();
            // Realiza o comando do query, o qual insere os dados nos campos correspondentes da tabela.
        }
        public void Atualizar(int id)
        {
            OleDbConnection con = new OleDbConnection(Conexao.Local());
            con.Open();
            string atualizeDatabase = "update Clientes set Nome = '" + Nome + "', Email = '" + Email + "', CPF = '" + CPF + "' Where ID = " + id;
            OleDbCommand cmd = new OleDbCommand(atualizeDatabase, con);
            cmd.ExecuteNonQuery();
            // Realiza o comando do query, o qual atualiza os dados nos campos correspondentes da tabela.
        }
        public void Deletar(int id)
        {
            OleDbConnection con = new OleDbConnection(Conexao.Local());
            con.Open();
            OleDbCommand cmd = new OleDbCommand("DELETE * FROM Clientes Where ID =" + id, con);
            cmd.ExecuteNonQuery();
            // Realiza o comando do query, o deleta da sua tabela aonde sua ID for igual a selecionada.
        }
        public static DataSet Procurar(int id)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(Conexao.Local());
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("Select ID as ID, Nome as Nome, Email as Email, CPF as CPF From Clientes Where ID = " + id, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "DGV1");
                return ds;
                // Dá um select nos dados e os mostra no seu DataGridView.
            }
            catch
            {
                throw;
            }
        }
    }
}
