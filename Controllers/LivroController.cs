using System;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Livro l)
        {
            LivroService livroService = new LivroService();

            if(l.Id == 0)
            {
                livroService.Inserir(l);
            }
            else
            {
                livroService.Atualizar(l);
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, string itensPorPagina, int NumDaPagina, int PaginaAtual)
        {
            Autenticacao.CheckLogin(this);
            FiltrosLivros objFiltro = null;
            if(!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosLivros();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }
                ViewData["livrosPorPagina"] =  (string .IsNullOrEmpty(itensPorPagina) ? 10 : Int32.Parse(itensPorPagina));
                ViewData["PaginaAtual"] = (PaginaAtual !=0 ? PaginaAtual : 1);

            LivroService livroService = new LivroService();
            return View(livroService.ListarTodos(objFiltro));
        }

        //listagem de acordo com o momento online (não deu tempo implementar):

        // public IActionResult Listagem (string tipoFiltro, string Filtro, int p = 1)
        // {
        //     Autenticacao.CheckLogin(this);
        //     Filtragem objFiltro = null;
        //     if (!string.IsNullOrEmpty(Filtro)){
        //         objFiltro = new Filtragem();
        //         objFiltro.Filtro = Filtro;
        //         objFiltro.TipoFiltro = TipoFiltro;
        //     }

        //     int quantidadePorPagina = 10;
        //     LivroService livroService = new LivroService();
        //     int totalDeRegistros = livroService.NumeroDeLivros();
        //     ICollection<Livro> lista = livroService.ListarTodos(p, quantidadePorPagina, objFiltro);
        //     ViewData["NroPagina"] = int Math.Ceiling((double) totalDeRegistros / quantidadePorPagina);
        //     return View(lista)
        // }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            LivroService ls = new LivroService();
            Livro l = ls.ObterPorId(id);
            return View(l);
        }
    }
}