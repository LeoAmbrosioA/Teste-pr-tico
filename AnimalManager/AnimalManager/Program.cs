using AnimalManager.Services;
using AnimalManager.Models;
using System;
using AnimalManager.AnimalManager.Services;

class Program
{
    static void Main()
    {
        DatabaseService.InitializeDatabase();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Gerenciador de Animais ===");
            Console.WriteLine("1. Listar Animais");
            Console.WriteLine("2. Cadastrar Animal");
            Console.WriteLine("3. Atualizar Animal");
            Console.WriteLine("4. Deletar Animal");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");

            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    ListarAnimais();
                    break;
                case "2":
                    CadastrarAnimal();
                    break;
                case "3":
                    AtualizarAnimal();
                    break;
                case "4":
                    DeletarAnimal();
                    break;
                case "5":
                    Console.WriteLine("Encerrando o programa...");
                    return;
                default:
                    Console.WriteLine("Opção inválida, tente novamente.");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    static void ListarAnimais()
    {
        var animais = AnimalService.ListarAnimais();
        if (animais.Count == 0)
        {
            Console.WriteLine("Nenhum animal encontrado.");
        } else
        {
            foreach (var animal in animais)
            {
                Console.WriteLine($"ID: {animal.Id}, Nome: {animal.Nome}, Idade: {animal.Idade}, Espécie: {animal.Especie}, Data Adoção: {animal.DataAdocao}");
            }
        }
    }

    static void CadastrarAnimal()
    {
        string nome;
        int idade;
        string especie;
        DateTime? dataAdocaoFormatada = null;

        do
        {
            Console.Write("Nome (ou digite 'cancelar' para sair): ");
            nome = Console.ReadLine();

            if (nome?.ToLower() == "cancelar")
            {
                Console.WriteLine("Operação cancelada.");
                return;
            }

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("O campo 'Nome' não pode estar vazio. Tente novamente.");
            }

        } while (string.IsNullOrWhiteSpace(nome));

        do
        {
            Console.Write("Idade (ou digite 'cancelar' para voltar ao menu): ");
            string idadeInput = Console.ReadLine();

            if (idadeInput?.ToLower() == "cancelar")
            {
                Console.WriteLine("Operação cancelada. Retornando ao menu principal...");
                return;
            }

            if (!int.TryParse(idadeInput, out idade) || idade < 0)
            {
                Console.WriteLine("O campo 'Idade' é obrigatório e deve ser um número inteiro positivo. Tente novamente.");
                idade = -1;
            }

        } while (idade < 0); ;

        do
        {
            Console.Write("Espécie (ou digite 'cancelar' para sair): ");
            especie = Console.ReadLine();

            if (especie?.ToLower() == "cancelar")
            {
                Console.WriteLine("Operação cancelada.");
                return;
            }

            if (string.IsNullOrWhiteSpace(especie))
            {
                Console.WriteLine("O campo 'Espécie' não pode estar vazio. Tente novamente.");
            }

        } while (string.IsNullOrWhiteSpace(especie));

        while (true)
        {
            Console.Write("Data de Adoção (pressione Enter para usar a data atual ou insira no formato yyyy-MM-dd, ou digite 'cancelar'): ");
            string dataAdocao = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(dataAdocao))
            {
                dataAdocaoFormatada = DateTime.Now;  // Usar data atual se o campo estiver vazio
                break;
            } else if (dataAdocao.ToLower() == "cancelar")
            {
                Console.WriteLine("Operação cancelada. Retornando ao menu principal...");
                return;
            } else if (DateTime.TryParse(dataAdocao, out DateTime data))
            {
                dataAdocaoFormatada = data;
                break;
            } else
            {
                Console.WriteLine("Formato inválido. Insira uma data válida no formato yyyy-MM-dd ou digite 'cancelar'.");
            }
        }

        var animal = new Animal
        {
            Nome = nome,
            Idade = idade,
            Especie = especie,
            DataAdocao = dataAdocaoFormatada
        };

        AnimalService.AddAnimal(animal);
        Console.WriteLine("Animal cadastrado com sucesso!");
    }

    static void AtualizarAnimal()
    {
        Console.Write("Digite o ID do animal a ser atualizado (ou 'cancelar' para voltar ao menu principal): ");
        string idInput = Console.ReadLine();

        if (idInput.ToLower() == "cancelar")
        {
            Console.WriteLine("Operação cancelada. Voltando ao menu principal...");
            return;
        }

        if (!int.TryParse(idInput, out int id))
        {
            Console.WriteLine("ID inválido. Operação cancelada.");
            return;
        }

        var animal = AnimalService.BuscarAnimalPorId(id);

        if (animal == null)
        {
            Console.WriteLine("Animal não encontrado.");
            return;
        }

        Console.Write("Novo Nome (ou deixe em branco para não alterar, ou 'cancelar' para voltar ao menu): ");
        string nome = Console.ReadLine();
        if (nome.ToLower() == "cancelar")
        {
            Console.WriteLine("Operação cancelada. Voltando ao menu principal...");
            return;
        }
        if (!string.IsNullOrWhiteSpace(nome))
        {
            animal.Nome = nome;
        }

        Console.Write("Nova Idade (ou deixe em branco para não alterar, ou 'cancelar' para voltar ao menu): ");
        string idadeInput = Console.ReadLine();
        if (idadeInput.ToLower() == "cancelar")
        {
            Console.WriteLine("Operação cancelada. Voltando ao menu principal...");
            return;
        }
        if (!string.IsNullOrWhiteSpace(idadeInput) && int.TryParse(idadeInput, out int idade) && idade >= 0)
        {
            animal.Idade = idade;
        }

        Console.Write("Nova Espécie (ou deixe em branco para não alterar, ou 'cancelar' para voltar ao menu): ");
        string especie = Console.ReadLine();
        if (especie.ToLower() == "cancelar")
        {
            Console.WriteLine("Operação cancelada. Voltando ao menu principal...");
            return;
        }
        if (!string.IsNullOrWhiteSpace(especie))
        {
            animal.Especie = especie;
        }

        Console.Write("Nova Data de Adoção (yyyy-MM-dd, NOW, ou deixe em branco para não alterar, ou 'cancelar' para voltar ao menu): ");
        string dataAdocao = Console.ReadLine();
        if (dataAdocao.ToLower() == "cancelar")
        {
            Console.WriteLine("Operação cancelada. Voltando ao menu principal...");
            return;
        }
        if (string.IsNullOrWhiteSpace(dataAdocao))
        {
            Console.WriteLine("Data de adoção não alterada.");
        }
        else if (dataAdocao.ToUpper() == "NOW")
        {
            animal.DataAdocao = DateTime.Now;
            Console.WriteLine("Data de adoção alterada para o momento atual.");
        }
        else if (DateTime.TryParse(dataAdocao, out DateTime data))
        {
            animal.DataAdocao = data;
            Console.WriteLine("Data de adoção alterada para: " + data.ToString("yyyy-MM-dd"));
        }
        else
        {
            Console.WriteLine("Formato inválido de data. Operação cancelada.");
            return;
        }


        AnimalService.AtualizarAnimal(animal);
        Console.WriteLine("Animal atualizado com sucesso!");
    }

    static void DeletarAnimal()
    {
        Console.Write("Digite o ID do animal a ser deletado: ");
        int id = int.Parse(Console.ReadLine());
        AnimalService.DeletarAnimal(id);
    }
}