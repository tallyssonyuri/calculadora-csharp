using System;
using System.Collections;
using System.Collections.Generic;

namespace Calculadora {
    class Program {
        static string ObterNomeOperacao(char operador) {
            switch (operador) {
                case '+': return "Soma";
                case '-': return "Subtração";
                case '*': return "Multiplicação";
                case '/': return "Divisão";
                default: return "Operação";
            }
        }
        
        static void Main(string[] args) {
            Queue<Operacoes> filaOperacoes = new Queue<Operacoes>();

            filaOperacoes.Enqueue(new Operacoes { valorA = 2, valorB = 3, operador = '+' });
            filaOperacoes.Enqueue(new Operacoes { valorA = 14, valorB = 8, operador = '-' });
            filaOperacoes.Enqueue(new Operacoes { valorA = 5, valorB = 6, operador = '*' });
            filaOperacoes.Enqueue(new Operacoes { valorA = 2147483647, valorB = 2, operador = '+' });
            filaOperacoes.Enqueue(new Operacoes { valorA = 18, valorB = 3, operador = '/' });  // Implementação do calculo de divisão

            Calculadora calculadora = new Calculadora();
            Stack<decimal> pilhaResultados = new Stack<decimal>();
            List<string> todosCalculos = new List<string>();

            Console.WriteLine("CALCULADORA EM C# - CASE PARA ENGENHARIA DE SOFTWARE JR\n");
            Console.WriteLine("A seguir, todas as operações serão processadas uma a uma, seguindo a ordem de chegada na fila. O resultado de cada operação será impresso no console e ao final, a pilha com todos os resultados será impressa");
            Console.WriteLine(new string('-', Console.WindowWidth));
            
            int numeroOperacao = 1;

            while (filaOperacoes.Count > 0) {              
                Operacoes operacao = filaOperacoes.Dequeue();
                calculadora.calcular(operacao);
                Console.WriteLine("{0}ª operação: {1} {2} {3} = {4}\n", numeroOperacao, operacao.valorA, operacao.operador, operacao.valorB, operacao.resultado);

                pilhaResultados.Push(operacao.resultado);
                todosCalculos.Add(string.Format("{0} {1} {2} = {3}", operacao.valorA, operacao.operador, operacao.valorB, operacao.resultado));
                numeroOperacao++;
                
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
            }
            
            // Todos os cáculos realizados
            Console.WriteLine("RESUMO DE TODOS OS CÁLCULOS REALIZADOS:\n");
            foreach (string calculo in todosCalculos) {
                Console.WriteLine(calculo);
            }
            Console.WriteLine(new string('-', Console.WindowWidth));
            
            // Pilha de resultados
            Console.WriteLine("PILHA DE RESULTADOS (do mais recente para o mais antigo):");
            int posicao = 1;

            while (pilhaResultados.Count > 0) {
                decimal resultado = pilhaResultados.Pop();
                Console.WriteLine("{0}°. {1}", posicao, resultado);
                posicao++;
            }
        }
    }
}
