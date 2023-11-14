using Microsoft.AspNetCore.Mvc.Rendering;

namespace Estoque.Models
{
    
    public class ItensEstoque
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Setor { get; set; } // Alteração da propriedade de Descrição para Setor
        public int Quantidade { get; set; }


        // Propriedade que retorna true se a quantidade for menor ou igual a 10
        public bool PrecisaReabastecer => Quantidade <= 10;

        // Método que retorna uma mensagem sugerindo reabastecimento se a quantidade for <= 10
        public string VerificarReabastecimento()
        {
            if (Quantidade <= 10)
            {
                return $"A quantidade de {Nome} no setor {Setor} está baixa. Por favor, reabasteça o estoque.";
            }
            else
            {
                return $"A quantidade de {Nome} no setor {Setor} está adequada.";
            }
        }
    }

}
