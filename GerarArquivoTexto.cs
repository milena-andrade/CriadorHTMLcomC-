using System;
using System.IO;


namespace TrabalhoPRES
{
    class Program{


            public static void Main()
            {

            string inputFile = "arquivo.txt";
            string outputFile = "saida.html";

                    // Lê o arquivo de entrada usando StreamReader
                    string texto;
                    using (StreamReader reader = new StreamReader(inputFile))
                    {
                        texto = reader.ReadToEnd();
                    }

                    // Processa o texto para criar o conteúdo Txt
                    string conteudoTxt = ProcessarTextoTxt(texto);

                    // Escreve o conteúdo Txt no arquivo de entrada usando StreamWriter
                    using (StreamWriter writer = new StreamWriter(inputFile))
                    {
                        writer.Write(conteudoTxt);
                    }

                    // Processa o texto para criar o conteúdo HTML
                    string conteudoHtml = ProcessarTextoHtml(texto);

                    // Escreve o conteúdo HTML no arquivo de saída usando StreamWriter
                    using (StreamWriter writer = new StreamWriter(outputFile))
                    {
                        writer.Write(conteudoHtml);
                    }

                    Console.WriteLine("Arquivo HTML gerado com sucesso!");


            }


            static string ProcessarTextoTxt(string texto)
            {
                // Divide o texto em linhas
                string[] linhas = texto.Split('\n');



                // Cria o início do conteúdo Txt
                string conteudoTxt =                  "\n" +

                 "|!                                      'Bolos'                                          |\n" +
                 "|!                                                                                          |\n" +
                 "\n" +
                 "|                                 'Boulinhos'                                             |\n" +
                 "|                                                                                           |\n" +
                 "|                         .XXXXXXXXX.                 %Enviar%                              |\n"+
                 "|                                                                                           |\n" +
                 "|                                      Qual doce você gostaria?                             |\n" +
                 "|                            (Bolo, Brownie, Muffin, Cupcake, Donut)                        |\n" +
                 "|                                                                                           |\n" +
                 "|                                      Recheio:                                             |\n" +
                 "|                                    {Chocolate, Morango, Creme}                            |\n" +
                 "|                                                                                           |\n" +
                 "|                                  Gostaria de qual cobertura?                              |\n"+
                 "|                    [Sem cobertura,Chocolate ao leite,Chocolate branco,Glacê]              |\n" +
                 "|                                                                                           |\n" +
                 "|                                                                                           |\n" +
                 "|                                                                                           |\n" +
                 "|                                                                                           |\n" +
                 "|                                                                                           |\n" +
                 "!                                                                                           \n" ;


                return conteudoTxt;


            }


            static string ProcessarTextoHtml(string texto)

            {
                // Divide o texto em linhas
                string[] linhas = texto.Split('\n');
                string caracter = "";
                texto = texto.Replace(" ", "&nbsp;");

                // Cria o início do conteúdo HTML
                string conteudoHtml = "<!DOCTYPE html>\n" +
                "<html>\n" +
                "<head> \n" +
                "<title>Arquivo HTML</title>\n" +
                "<style>\n" +
                "pre { white-space: pre-wrap; font-family: monospace; }\n" +
                "h1 { color: #1d3c50; }\n" +
                "body { text-align: center; background-color: #e8f1f2; }\n"+
                ".left { text-align: left; }\n" +
                ".right { text-align: right; }\n" +
                ".botao {\n" +
                "    background-color: #187daf;\n" +
                "    border: none;\n" +
                "    color: white;\n" +
                "    padding: 10px 20px;\n" +
                "    text-align: center;\n" +
                "    text-decoration: none;\n" +
                "    display: inline-block;\n" +
                "    font-size: 16px;\n" +
                "    margin: 4px 2px;\n" +
                "    cursor: pointer;\n" +
                "}\n" +
                "</style>\n" +
                "</head>\n" +
                "<body>\n";


                    // Processa cada linha do texto
                    foreach (string linha in linhas)
                    {
                        // Remove espaços em branco no início e no final da linha
                        string linhaProcessada = linha.Trim();
                        int tam = linhaProcessada.Length;

                       linhaProcessada = linhaProcessada.Replace("<", "&lt;")
                                         .Replace(">", "&gt;")
                                         .Replace("&", "&amp;");



                        // Remove o símbolo "|"
                        if(linhaProcessada.Contains("|")){
                            linhaProcessada = linhaProcessada.Replace("|"," ");
                        }

                        //caixa de texto, combobox, checkbox, radiobutton, button

                        //Verifica se é um cabeçalho de nível 1
                        if (linhaProcessada.Contains("'"))
                        {
                            int inicio = linhaProcessada.IndexOf("'");
                            int fim = linhaProcessada.IndexOf("'", inicio + 1);
                            string textoEntreAspas = linhaProcessada.Substring(inicio + 1, fim - inicio - 1);

                            linhaProcessada = linhaProcessada.Replace("'" + textoEntreAspas + "'", " &#x1F370 <h1> " + textoEntreAspas + "</h1>");
                            Console.WriteLine(linhaProcessada);
                        }
                        //Verifica se é uma combobox
                        else
                        if (linhaProcessada.Contains("(") && linhaProcessada.Contains(")"))
                        {
                            int inicio = linhaProcessada.IndexOf("(");
                            int fim = linhaProcessada.IndexOf(")");
                            string textoEntreParenteses = linhaProcessada.Substring(inicio + 1, fim - inicio - 1);

                            // Separa os itens da lista pelo caractere ',' e remove espaços em branco
                            string[] itens = textoEntreParenteses.Split(',').Select(item => item.Trim()).ToArray();

                            // Cria as opções da lista de seleção com base nos itens
                            string opcoes = string.Join("", itens.Select(item => $"<option value='{item}'>{item}</option>"));

                            linhaProcessada = linhaProcessada.Replace("(" + textoEntreParenteses + ")", $"<select name='info[]' size='3' multiple>{opcoes}</select>");
                        }

                        Console.WriteLine(linhaProcessada);



                        if (linhaProcessada.Contains("{") && linhaProcessada.Contains("}"))
                            {
                                int inicio = linhaProcessada.IndexOf("{");
                                int fim = linhaProcessada.IndexOf("}");
                                string textoEntreColchetes = linhaProcessada.Substring(inicio + 1, fim - inicio - 1);

                                // Separa as opções pelo caractere ',' e remove espaços em branco
                                string[] opcoes = textoEntreColchetes.Split(',').Select(opcao => opcao.Trim()).ToArray();

                                // Cria os inputs radio button com base nas opções
                                string inputsRadioButton = "";
                                foreach (string opcao in opcoes)
                                {
                                    inputsRadioButton += $"<label><input type='radio' name='opcoes' value='{opcao}'> {opcao}</label>";
                                }

                                linhaProcessada = linhaProcessada.Replace("{" + textoEntreColchetes + "}", inputsRadioButton);
                            }


                        // Verifica se é um checkbox
                        if (linhaProcessada.Contains("[") && linhaProcessada.Contains("]"))
                        {
                            int inicio = linhaProcessada.IndexOf("[");
                            int fim = linhaProcessada.IndexOf("]");
                            string textoEntreColchetes = linhaProcessada.Substring(inicio + 1, fim - inicio - 1);

                            // Separa as opções pelo caractere ',' e remove espaços em branco
                            string[] opcoes = textoEntreColchetes.Split(',').Select(opcao => opcao.Trim()).ToArray();

                            // Cria os inputs checkbox com base nas opções
                            string inputsCheckbox = "";
                            foreach (string opcao in opcoes)
                            {
                                inputsCheckbox += $"<label><input type='checkbox' name='opcoes' value='{opcao}'> {opcao}</label>";
                            }

                            linhaProcessada = linhaProcessada.Replace("[" + textoEntreColchetes + "]", inputsCheckbox);
                        }


                        if (linhaProcessada.Contains("!") && linhaProcessada.Contains("!"))
                        {
                            int inicio = linhaProcessada.IndexOf("!");
                            int fim = linhaProcessada.IndexOf("!");
                            //string textoEntreParenteses = linhaProcessada.Substring(inicio + 1, fim - inicio - 1);

                            linhaProcessada = linhaProcessada.Replace("!", "<hr>");
                            Console.WriteLine(linhaProcessada);
                        }




                        int i = 0;

                        //Procura por determinado caractere
                        while (i < tam)
                        {
                            caracter = linhaProcessada.Substring(i,1);

                            //Verifica se é um textbox
                            if (caracter == ".")
                            {
                                Console.WriteLine("encontrou" + i);
                                int inicio = linhaProcessada.IndexOf(".");
                                int fim = linhaProcessada.IndexOf(".", inicio + 1);
                                string textoEntreAspas = linhaProcessada.Substring(inicio + 1, fim - inicio - 1);

                                linhaProcessada = linhaProcessada.Replace("." + textoEntreAspas + ".", "Nome: <input type='text' name='nome'>");
                                Console.WriteLine(linhaProcessada);

                            }
                            //Verifica se um botão
                            else  if (caracter == "%")
                            {
                                Console.WriteLine("encontrou" + i);
                                int inicio = linhaProcessada.IndexOf("%");
                                int fim = linhaProcessada.IndexOf("%", inicio + 1);
                                string textoEntreAspas = linhaProcessada.Substring(inicio + 1, fim - inicio - 1);

                                linhaProcessada = linhaProcessada.Replace("%" + textoEntreAspas + "%", "<button class='botao' type='submit' name='botao'>" + textoEntreAspas + "</button>");
                                Console.WriteLine(linhaProcessada);

                            }
                                i++;
                        }

                        // Adiciona a linha processada como um parágrafo no conteúdo HTML
                        conteudoHtml += "<pre>" + linhaProcessada + "</pre>\n";

                    }


                // Adiciona o fechamento do conteúdo HTML
                conteudoHtml += "</body>\n" +
                    "</html>";


                return conteudoHtml;
            }


    }


}