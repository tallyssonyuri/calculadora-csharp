# Calculadora
Case para Engenharia de Software JR

Você precisa corrigir os seguintes problemas no codigo:
  1. Aplicação só está processando o primeiro item da fila infinitamente.
  2. Implemente a funcionalidade de divisão.
  3. Aplicação não está calculando a penultima operação corretamente.
     
     	Saída esperada no console:
     
     		14 - 8 = 6
     
     		5 * 6 = 30
     
     		2147483647 + 2 = 2147483649
     
     		18 / 3 = 6

  5. Implemente uma funcionalidade para imprimir toda a lista de operaçõoes a ser processada após cada calculo realizado.
  6. Crie uma nova pilha (Stack) para guardar o resultado de cada calculo efetuado e imprima a pilha ao final


Não existe resposta certa ou errada, o objetivo do case é avaliar a linha de raciocínio de cada candidato.
Você é livre para fazer na linguagem de sua preferência, desde que aplique as mesmas funcionalidades e tarefas deste case.
Dica: Utilize Visual Code ou Visual Studio Community para realizar as tarefas.

---

## Soluções Implementadas

### 1. Loop Infinito
O código usava `Peek()` que não remove itens da fila, causando loop infinito. Então foi substituído por `Dequeue()` que remove e processa cada item:
```csharp
while (filaOperacoes.Count > 0) {              
    Operacoes operacao = filaOperacoes.Dequeue();
    calculadora.calcular(operacao);
    Console.WriteLine("{0}ª operação: {1} {2} {3} = {4}\n", numeroOperacao, operacao.valorA, operacao.operador, operacao.valorB, operacao.resultado);
    pilhaResultados.Push(operacao.resultado);
    numeroOperacao++;
}
```

### 2. Divisão Não Implementada
Foi criado método de divisão, além de um tratamento de erro caso o denominador seja zero:
```csharp
public long divisao(Operacoes operacao) {
    if (operacao.valorB == 0) {
        throw new DivideByZeroException("Erro: Divisão por zero não é permitido");
    }
    return operacao.valorA / operacao.valorB;
}
```

### 3. Cálculos Incorretos
As variáveis eram `int` causando overflow na operação `2147483647 + 2`, pois a variável não suportaria o tamanho de dados a ser armazenado. Elas foram alteradas para `long` nas propriedades `valorA` e `valorB` da classe `Operacoes`, para que assim não haja mais esse problema:
```csharp
public long valorA {get;set;}
public char operador { get; set; }
public long valorB { get; set; }
public decimal resultado { get; set; }
```

#### Saída Esperada
- 14 - 8 = 6
- 5 * 6 = 30  
- 2147483647 + 2 = 2147483649
- 18 / 3 = 6

### 4. Lista de Operações Restantes
Não mostrava operações pendentes na fila. Foi implementado um loop com função `ObterNomeOperacao()`:
```csharp
if (filaOperacoes.Count > 0) {
    Console.WriteLine("Operações restantes na fila:");
    int contador = 1;
    foreach (Operacoes op in filaOperacoes) {
        Console.WriteLine("{0}. {1}: {2} {3} {4}", contador, ObterNomeOperacao(op.operador), op.valorA, op.operador, op.valorB);
        contador++;
    }
    Console.WriteLine(new string('-', Console.WindowWidth));
} else {
    Console.WriteLine("Não há mais operações na fila");
    Console.WriteLine(new string('-', Console.WindowWidth));
}
```

### 5. Pilha de Resultados
Não havia estrutura para armazenar resultados. Foi criada uma `Stack<decimal>` com `Push()` e `Pop()`:
```csharp
Stack<decimal> pilhaResultados = new Stack<decimal>();
pilhaResultados.Push(operacao.resultado);

Console.WriteLine("PILHA DE RESULTADOS (do mais recente para o mais antigo):");
int posicao = 1;
while (pilhaResultados.Count > 0) {
    decimal resultado = pilhaResultados.Pop();
    Console.WriteLine("{0}°. {1}", posicao, resultado);
    posicao++;
}
```

## Melhorias Adicionais

- Adicionada numeração das operações (1ª operação, 2ª operação, etc.)
- Criadas linhas separadoras adaptáveis utilizando `new string('-', Console.WindowWidth)`
- Implementado resumo de todos os cálculos realizados
