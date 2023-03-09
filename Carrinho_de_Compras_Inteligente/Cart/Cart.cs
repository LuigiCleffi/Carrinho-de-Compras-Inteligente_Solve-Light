using System.Text;
using System.Xml.Linq;

namespace Carrinho_de_Compras_Inteligente.Cart
{
    public class Cart
    {
        private Dictionary<string, double> cartItems = new Dictionary<string, double>();
        private bool finalizarCompra = false;
        public void Services()
        {
            Console.WriteLine(this.ToString());
            char cartOption = '0';

            while (!finalizarCompra)
            {
                try
                {

                    Console.WriteLine("Comprar mais produtos? 1 - Sim | 2 - Finalizar Compra\n");
                    string input = Console.ReadLine();
                    if (input.Length == 0)
                    {
                        throw new IndexOutOfRangeException("Ops input vazio!");
                    }

                    cartOption = input[0];
                    switch (cartOption)
                    {
                        case '1':
                            if (cartItems.Count <= 10)
                            {
                                AddCartItem();
                            }
                            else
                            {
                                Console.WriteLine("Você atingiu o limite máximo de itens no carrinho. (10)\n");
                                Console.WriteLine("--------------------------------------------------------\n");
                                Console.WriteLine("Deseja acessar o menu ? 1- Sim | 2 - Finalizar Compra \n");
                                string optionMenu = Console.ReadLine();
                                cartOption = optionMenu[0];

                                switch (cartOption)
                                {
                                    case '1':
                                        Menu();
                                        break;
                                    case '2':
                                        EndServices();
                                        finalizarCompra = true;
                                        break;
                                    default:
                                        Console.WriteLine("Ops opção inválida");
                                        continue;
                                }
                            }
                            break;
                        case '2':
                            EndServices();
                            finalizarCompra = true;
                            break;
                        default:
                            Console.WriteLine("Ops valor incorreto, tente 1 ou 2");
                            continue;
                    }

                }


                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void Menu()
        {
            Console.Clear();
            char menuOption = '0';

            while (!finalizarCompra)
            {
                try
                {
                    Console.WriteLine("=============================");
                    Console.WriteLine("== 1 - Editar Item ==========");
                    Console.WriteLine("== 2 - Remover Item =========");
                    Console.WriteLine("== 3 - Limpar carrinho ======");
                    Console.WriteLine("== 4 - Status do carrinho ===");
                    Console.WriteLine("== 5 - Procurar item ========");
                    Console.WriteLine("== 6 - Salvar Carrinho ======");
                    Console.WriteLine("== 7 - Atualizar quantidade =");
                    Console.WriteLine("== 8 - Finalizar ============");
                    Console.WriteLine("=============================");

                    Console.Write("Selecione uma opção: ");

                    string input = Console.ReadLine();
                    if (input.Length == 0)
                    {
                        throw new IndexOutOfRangeException("Ops input vazio!");
                    }

                    menuOption = input[0];
                    switch (menuOption)
                    {
                        case '1':
                            EditItem();
                            break;
                        case '2':
                            RemoveCartItem();
                            break;
                        case '3':
                            ClearCart();
                            break;
                        case '4':
                            GetCartStatus();
                            break;
                        case '5':
                            SearchCartItem();
                            break;
                        case '6':
                            SaveCart();
                            break;
                        case '7':
                            changeAmountOfItems();
                            break;
                        case '8':
                            EndServices();
                            finalizarCompra = true;
                            break;
                        default:
                            Console.WriteLine("Ops opção inválida");
                            continue;
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }

        private void SaveCart()
        {
            string cartData = GetCartData();
            string fileName = "cartData.txt";
            string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\" + fileName;

            try
            {
                using StreamWriter writer = new(downloadPath);
                writer.Write(cartData);
                Console.WriteLine($"Dados do carrinho salvos em {downloadPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar o arquivo: {ex.Message}");
            }
        }

        private void changeAmountOfItems()
        {
            Console.Write("Nome do produto: ");
            string itemName = Console.ReadLine();

            if (cartItems.ContainsKey(itemName))
            {
                Console.WriteLine("Produto encontrado\n");
                Console.WriteLine("Quantos itens você deseja adicinar ? \n");
                int itemAmount= int.Parse(Console.ReadLine());

                cartItems[itemName] *=  itemAmount;
                Console.WriteLine("Quantidade de itens atualizada com sucesso! \n");
                Console.WriteLine("... Voltando ao menu ...\n\n");
                Console.WriteLine("Pressione qualquer tecla para continuar.");

                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("Ops infelizmente produto não encontrado !");
                Console.WriteLine("... Voltando ao menu ...\n\n");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
        }

        private void SearchCartItem()
        {
            Console.Write("Nome do produto: ");
            string itemName = Console.ReadLine();

            if (cartItems.ContainsKey(itemName))
            {
                Console.WriteLine("Produto encontrado\n");
                Console.WriteLine("... Voltando ao menu ...\n\n");
                Console.WriteLine("Pressione qualquer tecla para continuar.");

                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("Ops infelizmente produto não encontrado !");
                Console.WriteLine("... Voltando ao menu ...\n\n");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
        }

        private void GetCartStatus()
        {
            if (cartItems.Count == 0)
            {
                Console.WriteLine("Seu carrinho está vazio.");
            }
            else
            {
                Console.WriteLine($"Seu carrinho possui {cartItems.Count} item(s) no valor total de R${CalculateTotal():0.00}."); 
            }
        }

        private void ClearCart()
        {

            Console.WriteLine("Carrinho limpado com sucesso");
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }

        private void EditItem()
        {
            Console.WriteLine("Nome do produto");
            string produto = Console.ReadLine();

            if (cartItems.ContainsKey(produto))
            {
                Console.WriteLine($"Produto {produto} encontrado! ");
                Console.Write("Digite um novo valor para o produto: ");
                double newItemPrice = double.Parse(Console.ReadLine());
                cartItems[produto] = newItemPrice;
                Console.WriteLine($"Produto '{produto}' atualizado para R${newItemPrice:F2}.\n");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                Console.Clear();
            } else
            {
                Console.WriteLine($"Produto '{produto}' não encontrado no carrinho.\n");
            }
        }

        private void RemoveCartItem()
        {
            Console.Write("Digite o nome do produto que deseja remover: ");
            string itemName = Console.ReadLine();

            if (cartItems.ContainsKey(itemName))
            {
                cartItems.Remove(itemName);
                Console.WriteLine($"Produto '{itemName}' removido do carrinho.\n");
            }
            else
            {
                Console.WriteLine($"Produto '{itemName}' não encontrado no carrinho.\n");
            }

            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }
        private void EndServices()
        {
            Console.WriteLine($"ValorTotal: R${CalculateTotal():F2}\n") ;
            CartItems();
            handleCedules();
            Console.WriteLine("... Encerrando a aplicação ...");
            Console.ReadKey();
        }

        private void CartItems()
        {
            Console.WriteLine("\n======= Itens no carrinho =======");
            Console.WriteLine("_________________________________\n");

            foreach (var item in cartItems)
            {
                Console.WriteLine($"{item.Key} - R${item.Value:F2}\n");
            }
        }

        private string GetCartData()
        {
            StringBuilder data = new StringBuilder();

            foreach (var item in cartItems)
            {
                data.AppendLine($"{item.Key},{item.Value}");
            }

            return data.ToString();
        }


        private void AddCartItem()
        {

            try
            {
                Console.Write("Nome do Produto: ");
                string item = Console.ReadLine();

                Console.Write("Valor: ");
            
                double price = double.Parse(Console.ReadLine());


                if (cartItems.ContainsKey(item))
                {
                    Console.WriteLine($"O produto '{item}' já existe no carrinho.");
                }
                else
                {
                    cartItems.Add(item, price);
                    Console.WriteLine($"Produto '{item}' adicionado ao carrinho por R${price:F2}\n");
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Valor inválido. Tente novamente.");
            }
        }

        private void handleCedules()
        {

            int[] cedules = { 100, 50, 20, 10, 5, 2, 1 };
            int[] amountOfCedules = new int[cedules.Length];

            int totalAmount = (int)CalculateTotal();

            for (int i = 0; i < cedules.Length; i++)
            {
                amountOfCedules[i] = (int)(CalculateTotal() / cedules[i]);
                totalAmount -= amountOfCedules[i] * cedules[i];

                if (amountOfCedules[i] > 0)
                {
                    Console.WriteLine($"Notas de {cedules[i]} reais: {amountOfCedules[i]}\n");
                }
            }
        }
        private double CalculateTotal()
        {
            double total = 0;
            foreach (var item in cartItems)
            {
                total += item.Value;
            }
            return total;
        }

        public override string ToString()
        {
            return "===================" +
                "\n===  Carrinho  ====" +
                "\n===================";
        }
        
    }

}
