//using Biblioteca.Enums;
//using Biblioteca.Models;
//using Biblioteca.Repositories;
//using Biblioteca.Repositories.Interfaces;
//using Biblioteca.Areas.Administration.Services.Interfaces;
//using Biblioteca.Areas.Administration.ViewModels;
//using Microsoft.EntityFrameworkCore.Storage.Json;
//using System.Runtime.ConstrainedExecution;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace Biblioteca.Areas.Administration.Services
//{
//    public class EnderecoService : IEnderecoService
//    {
//        private readonly IEnderecoRepository _enderecoRepository;
//        private readonly IUsuarioRepository _usuarioRepository;
//        public EnderecoService(IEnderecoRepository enderecoRepository, IUsuarioRepository usuarioRepository)
//        {
//            _enderecoRepository = enderecoRepository;
//            _usuarioRepository = usuarioRepository;
//        }
//        public async Task<Endereco> AtualizarEndereco(Guid id, EnderecoViewModel endereco)
//        {
//            Endereco? enderecoRegistrado = await _enderecoRepository.GetEnderecoById(id);

//            if (enderecoRegistrado is not null && enderecoRegistrado.Deletado == false)
//            {
//                enderecoRegistrado.Logradouro = endereco.Logradouro;
//                enderecoRegistrado.TipoLogradouro = endereco.TipoLogradouro;
//                enderecoRegistrado.Numero = endereco.Numero;
//                enderecoRegistrado.UsuarioId = endereco.UsuarioId;
//                enderecoRegistrado.Cep = endereco.Cep;
//                enderecoRegistrado.Complemento = endereco.Complemento;

//                await _enderecoRepository.AtualizarEndereco(enderecoRegistrado);

//                return enderecoRegistrado;
//            }

//            throw new ArgumentException("O endereço não pôde ser atualizado pois não existe no banco de dados ou possui um Id diferente.");
//        }

//        public async Task<Endereco> CriarEndereco(EnderecoViewModel endereco)
//        {
//            if(_usuarioRepository.GetUsuarioById(endereco.UsuarioId) is null)
//            {
//                throw new ArgumentException("O usuário atrelado a esse endereço não existe. O endereço não pôde ser registrado.");
//            }

//            Endereco? novoEndereco = new Endereco()
//            {
//                Logradouro = endereco.Logradouro,
//                TipoLogradouro = endereco.TipoLogradouro,
//                Numero = endereco.Numero,
//                Complemento = endereco.Complemento,
//                Cep = endereco.Cep,
//                UsuarioId = endereco.UsuarioId
//            };

//            if(endereco is not null)
//            {
//                await _enderecoRepository.CriarEndereco(novoEndereco);

//                return novoEndereco;
//            }

//            throw new ArgumentException("Não foi possível criar o endereço (nulo após preenchimento dos dados).");
//        }

//        public async Task<Endereco> DeletarEndereco(Guid id)
//        {
//            Endereco? endereco = await _enderecoRepository.GetEnderecoById(id);

//            if(endereco is null || endereco.Deletado == true)
//            {
//                throw new ArgumentException("O endereço não pôde ser deletado porque já não existe no banco de dados.");
//            }

//            endereco.Deletado = true;
//            await _enderecoRepository.DeletarEndereco(endereco);

//            return endereco;
//        }

//        public async Task<Endereco?> GetEnderecoById(Guid id)
//        {
//            Endereco? endereco = await _enderecoRepository.GetEnderecoById(id);

//            if (endereco is null || endereco.Deletado == true)
//            {
//                throw new ArgumentException("Não existe um endereço com este Id no banco de dados.");
//            }

//            return endereco;
//        }

//        public async Task<IEnumerable<Endereco>> GetEnderecos()
//        {
//            IEnumerable<Endereco> enderecos = await _enderecoRepository.GetEnderecos();

//            enderecos = enderecos.Except(enderecos.Where(endereco => endereco.Deletado == true));

//            return enderecos;
//        }
//    }
//}
