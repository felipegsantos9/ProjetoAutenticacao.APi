namespace ProjetoAutenticacao.APi.Models.DTOs
{
    public class EmitirPassageiroDto
    {
        public int VooId { get; set; }
        public int PassageiroId { get; set; }
        public int Assento { get; set; }
    }

    public class PassagemDetalheDto
    {
        public int Id { get; set; }
        public string CodigoVoo { get; set; }
        public string Trajeto { get; set; }
        public string NomePassageiro { get; set; }
        public int Assento { get; set; }
    }
}