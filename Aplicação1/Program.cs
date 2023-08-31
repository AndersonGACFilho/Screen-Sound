//Screen Sound

using System;
using System.ComponentModel;
using System.Reflection.Metadata;

string screenSound = @"

█████████████████████████████████████████████████████████████████████████
█─▄▄▄▄█─▄▄▄─█▄─▄▄▀█▄─▄▄─█▄─▄▄─█▄─▀█▄─▄███─▄▄▄▄█─▄▄─█▄─██─▄█▄─▀█▄─▄█▄─▄▄▀█
█▄▄▄▄─█─███▀██─▄─▄██─▄█▀██─▄█▀██─█▄▀─████▄▄▄▄─█─██─██─██─███─█▄▀─███─██─█
█▄▄▄▄▄█▄▄▄▄▄█▄▄█▄▄█▄▄▄▄▄█▄▄▄▄▄█▄▄▄██▄▄███▄▄▄▄▄█▄▄▄▄██▄▄▄▄██▄▄▄██▄▄█▄▄▄▄██

";

List<Banda> listaDeBandas = new List<Banda>();

void Main()
{
    ExibirOpcoesDoMenu();
}

void PrintWelcomeMessage()
{
    Console.WriteLine(screenSound);
}

void ExibirOpcoesDoMenu()
{
    int opcaoInt = ImprimirOpcoesDoMenu();

    while (true)
    {
        switch (opcaoInt)
        {
            case 1:
                MostrarBandas();
                break;
            case 2:
                RegistrarBandas();
                break;
            case 3:
                AvaliarBandas();
                break;
            case 4:
                MostrarAvaliacao();
                break;
            case 5:
                Console.WriteLine("Você escolheu sair. Até mais!");
                return;
        }
        LimparTela();
        opcaoInt = ImprimirOpcoesDoMenu();
    }
}

int ImprimirOpcoesDoMenu()
{
    PrintWelcomeMessage();
    Console.WriteLine("Escolha uma opção:\n\r");

    Console.WriteLine("1 - Mostrar todas as bandas");
    Console.WriteLine("2 - Registrar uma banda");
    Console.WriteLine("3 - Avaliar uma banda");
    Console.WriteLine("4 - Mostar a avaliação de uma banda");
    Console.WriteLine("5 - Sair");

    Console.Write("\n\rDigite o número da opção desejada: ");
    string opcao;
    int opcaoInt;

    opcao = Console.ReadLine()!;

    while (true)
    {
        try
        {
            opcaoInt = int.Parse(opcao);

            if (opcaoInt is < 1 or > 5)
            {
                throw new Exception();
            }

            Console.WriteLine($"A opção escolhida foi: {opcaoInt}");
            break;

        }
        catch (Exception)
        {
            Console.WriteLine("Opção inválida. Digite novamente: ");
            opcao = Console.ReadLine()!;
        }
    }

    return opcaoInt;
}
void MostrarBandas()
{
    Console.WriteLine("\n\r");
    string titulo = "Bandas";
    ImprimirDivisoria(titulo);
    Console.WriteLine("");
    if (listaDeBandas.Count == 0)
    {
        Console.WriteLine("Nenhuma banda registrada");
        return;
    }
    listaDeBandas.ForEach(banda => Console.WriteLine(banda));
}   
void RegistrarBandas()
{
    Console.WriteLine("\n\r");
    string titulo = "Registrar uma banda";
    ImprimirDivisoria(titulo);
    Console.WriteLine();
    Console.Write("Digite o nome da banda: ");
    string nomeBanda = Console.ReadLine()!;
    if (listaDeBandas.Exists(banda => banda.getNome() == nomeBanda))
    {
        Console.WriteLine("Banda já registrada");
        return;
    }

    if (nomeBanda.Length < 4)
    {
        Console.WriteLine("Nome da banda deve ter no mínimo 4 caracteres");
        RegistrarBandas();
        return;
    }

    listaDeBandas.Add(new Banda(nomeBanda));
    Console.WriteLine($"Banda {nomeBanda} registrada com sucesso!");
}
void AvaliarBandas()
{
    Console.WriteLine("\n\r");
    string titulo = "Avaliar uma banda";
    ImprimirDivisoria(titulo);
    Console.WriteLine();
    Console.Write("Digite o nome da banda: ");
    string nomeBanda = Console.ReadLine()!;
    if (!listaDeBandas.Exists(banda => banda.getNome() == nomeBanda))
    {
        Console.WriteLine("Banda não registrada");
        return;
    }
    Console.Write("Digite a avaliação da banda: ");
    while (true)
    {
        try
        {
            int avaliacao = int.Parse(Console.ReadLine()!);
            Banda banda = listaDeBandas.Find(banda => banda.getNome() == nomeBanda);
            banda.addAvaliacao(avaliacao);
            break;
        }
        catch (Exception)
        {
            Console.WriteLine("Avaliação inválida. Digite novamente: ");
            nomeBanda = Console.ReadLine()!;
        }
    }

}
void MostrarAvaliacao()
{
    Console.WriteLine("\n\r");
    string titulo = "Mostrar a avaliação de uma banda";
    ImprimirDivisoria(titulo);
    Console.WriteLine();

    Console.Write("Digite o nome da banda: ");
    string nomeBanda = Console.ReadLine()!;
    if (!listaDeBandas.Exists(banda => banda.getNome() == nomeBanda))
    {
        Console.WriteLine("Banda não registrada");
        return;
    }
    Banda banda = listaDeBandas.Find(banda => banda.getNome() == nomeBanda);
    Console.WriteLine(banda);
}

void LimparTela(){

    Console.WriteLine("");
    string titulo = "Voltando ao menu...";
    Console.WriteLine(titulo);
    Console.WriteLine("");
    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();
    Console.Clear();
}

void ImprimirDivisoria(string titulo)
{
    int numeroDeAsteriscos = titulo.Length;
    string asteriscos = string.Empty.PadLeft(numeroDeAsteriscos, '*');

    Console.WriteLine(asteriscos);
    Console.WriteLine(titulo);
    Console.WriteLine(asteriscos);
    Console.WriteLine();
}

Main();

public class Banda
{
    private string nome { get; set; }
    private int avaliacao { get; set; }
    private int quantidadeAvaliacoes { get; set; }

    public Banda(string nome)
    {   
        this.nome = nome;
        this.avaliacao = 0;
        this.quantidadeAvaliacoes = 0;
    }

    public void addAvaliacao(int avaliacao)
    {
        this.avaliacao = (this.avaliacao*this.quantidadeAvaliacoes + avaliacao)/int.Abs(this.quantidadeAvaliacoes+1);
        this.quantidadeAvaliacoes++;
    }

    public override string ToString()
    {
        return $"Nome da banda: {this.nome} \r\n  Avaliação: {this.avaliacao} \r\n  Numero de avaliações: {this.quantidadeAvaliacoes}";
    }

    public string getNome()
    {
        return this.nome;
    }

    public int getAvaliacao()
    {
        return this.avaliacao;
    }
}