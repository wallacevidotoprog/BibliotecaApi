namespace BibliotecaApi.Infrastructure.Common
{
    public class ApiResponse<T>
    {
        public bool Sucesso { get; set; }
        public T? Conteudo { get; set; }
        public string? MensagemErro { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T>   
            {
                Sucesso = true,
                Conteudo = data
            };
        }

        public static ApiResponse<T> Error(string mensagem)
        {
            return new ApiResponse<T>
            {
                Sucesso = false,
                MensagemErro = mensagem
            };
        }
    }
}
