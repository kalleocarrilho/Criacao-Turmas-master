﻿using CriacaoTurmas.DAL;
using CriacaoTurmas.Model;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace CriacaoTurmas.View
{
    /// <summary>
    /// Interaction logic for CadastTurma.xaml
    /// </summary>
    public partial class CadastTurma : Window
    {
        public CadastTurma()
        {
            InitializeComponent();
            bindcombo(cboProfessor);
        }

        
        private void bindcombo(ComboBox cboProf)
        {
            
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["CriacaoTurmas.Properties.Settings.Configuração"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select nome,matricula FROM [Professor]";
            cmd.Connection = con;
    
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Professor");
            cboProf.ItemsSource = ds.Tables[0].DefaultView;
            cboProf.DisplayMemberPath = ds.Tables[0].Columns["nome"].ToString();
            cboProf.SelectedValuePath = ds.Tables[0].Columns["matricula"].ToString();

            

            

        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {

            // int cbobusca = Convert.ToInt32(cboProfessor.SelectedValue);
            Professor busca = new Professor();
            busca.matricula = Convert.ToInt32(cboProfessor.SelectedValue);

            


            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                Turma turma = new Turma
                {
                    Materia = txtNome.Text,
                    professor = ProfessorDAO.BuscarProfessorPorMatricula(busca)

            };

                if (TurmaDAO.AdicionarTurma(turma))
                {
                    
                    MessageBox.Show("Turma Gravada com Sucesso!");
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar turma!");
                }
            }
            else
            {
                MessageBox.Show("Favor preencher todos os campos!!");
            }


        }

        
    }
}
