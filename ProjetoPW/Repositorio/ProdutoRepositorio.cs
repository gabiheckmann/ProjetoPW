using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using ProjetoPW.Models;

namespace ProjetoPW.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        // Declara um campo privado somente leitura para armazenar a string de conexão com o MySQL.
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public Produto ObterProduto(string nome)
        {
            // Cria uma nova instância da conexão MySQL dentro de um bloco 'using'.
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL.
                conexao.Open();
                // Cria um novo comando SQL para selecionar todos os campos da tabela 'Produto' onde o campo 'Nome' corresponde ao parâmetro fornecido.
                MySqlCommand cmd = new("SELECT * FROM Produto WHERE Nome = @nome", conexao);
                // Adiciona um parâmetro ao comando SQL para o campo 'NOme', especificando o tipo como VarChar e utilizando o valor do parâmetro 'email'.
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;

                // Executa o comando SQL SELECT e obtém um leitor de dados (MySqlDataReader). O CommandBehavior.CloseConnection garante que a conexão
                // será fechada automaticamente quando o leitor for fechado.
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Inicializa uma variável 'produto' como null. Ela será preenchida se um produto for encontrado.
                    Produto produto = null;
                    // Lê a próxima linha do resultado da consulta. Retorna true se houver uma linha e false caso contrário.
                    if (dr.Read())
                    {
                        // Cria uma nova instância do objeto 'Produto'.
                        produto = new Produto
                        {
                            // Lê o valor da coluna "Id" da linha atual do resultado, converte para inteiro e atribui à propriedade 'Id' do objeto 'produto'.
                            IdProd = Convert.ToInt32(dr["Id"]),
                            // Lê o valor da coluna "Nome" da linha atual do resultado, converte para string e atribui à propriedade 'Nome' do objeto 'produto'.
                            Nome = dr["Nome"].ToString(),
                            // Lê o valor da coluna "Email" da linha atual do resultado, converte para string e atribui à propriedade 'Descricao' do objeto 'produto'.
                            Descricao = dr["Descricao"].ToString(),
                            // Lê o valor da coluna "Senha" da linha atual do resultado, converte para string e atribui à propriedade 'Preco' do objeto 'produto'.
                            Preco = Convert.ToDecimal(dr["Preco"]),
                            // Lê o valor da coluna "Senha" da linha atual do resultado, converte para string e atribui à propriedade 'Quantidade' do objeto 'produto'.
                            Quantidade = Convert.ToDecimal(dr["Quantidade"]),
                        };


                    }  /* Retorna o objeto 'produto'. Se nenhum usuário foi encontrado com o email fornecido, a variável 'produto'
                 permanecerá null e será retornado.*/
                    return produto;
                }


            }
        }
        public void AdicionarProduto(Produto produto)
        {
            /* Cria uma nova instância da conexão MySQL dentro de um bloco 'using'.
             Isso garante que a conexão será fechada e descartada corretamente após o uso, mesmo em caso de erro.*/
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL.

                conexao.Open();
                /* Cria um novo comando SQL para inserir dados na tabela 'Produto'. Os valores para Nome, Email e Senha são passados como parâmetros
                 (@Nome, @Descricao, @Preco,  @Quantidade) para evitar SQL Injection.*/

                MySqlCommand cmd = new("INSERT INTO Produto (Nome, Descricao, Preco,  Quantidade) VALUES (@Nome, @Descricao, @Preco,@Quantidade)", conexao);
                // Adiciona um parâmetro ao comando SQL para o campo 'Nome', utilizando o valor da propriedade 'Nome' do objeto 'produto'.
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                // Adiciona um parâmetro ao comando SQL para o campo 'Descricao', utilizando o valor da propriedade 'Descricao' do objeto 'produto'.
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                // Adiciona um parâmetro ao comando SQL para o campo 'Preco', utilizando o valor da propriedade 'Preco' do objeto 'produto'.
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                // Adiciona um parâmetro ao comando SQL para o campo 'Quantidade', utilizando o valor da propriedade 'Quantidade' do objeto 'produto'.
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                // Executa o comando SQL INSERT no banco de dados. Retorna o número de linhas afetadas.
                cmd.ExecuteNonQuery();
                // Fecha a conexão com o banco de dados (embora o 'using' já faria isso, só garante o fechamento).
                conexao.Close();
            }
        }
    }
}
