using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SetepassosPRJ.Models;

namespace SetepassosPRJ.Controllers
{
    public class JogoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult NovaTentativaAutonoma()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovaTentativaAutonomaAsync(Tentativa tentativa)
        {
            tentativa.IniciarTentativa();
            Repositorio.AddTentativa(tentativa);

            HttpClient client = MyHttpClient.Client;
            string path = "/api/NewGame";
            NewGameRequest newGameRequest = new NewGameRequest(tentativa.Nickname, tentativa.Classe);
            string json = JsonConvert.SerializeObject(newGameRequest);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode) { return Redirect("/"); }
            string json_r = await response.Content.ReadAsStringAsync();
            GameState gs = JsonConvert.DeserializeObject<GameState>(json_r);

            tentativa.AtualizarDados(gs);


            while (!tentativa.JogoFinalizado && gs.RoundNumber < 50)
            {
                tentativa.ExecutarAlgoritmo();
                if ((tentativa.Nickname == "auto3" && gs.RoundNumber >= 3) || (tentativa.Nickname == "auto7" && gs.RoundNumber >= 7))
                {
                    tentativa.Acao = Playeraction.Quit;

                    tentativa.FinalizarTentativa();
                }
                path = "/api/Play";
                PlayRequest playRequest = new PlayRequest(tentativa.Id, tentativa.Acao);
                json = JsonConvert.SerializeObject(playRequest);

                request = new HttpRequestMessage(HttpMethod.Post, path);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode) { return Redirect("/"); }
                json_r = await response.Content.ReadAsStringAsync();

                gs = JsonConvert.DeserializeObject<GameState>(json_r);

                if (tentativa.Acao != Playeraction.Quit)
                {
                    tentativa.AtualizarDados(gs);
                    tentativa.ExecutarAcao();
                }


                RoundSummary rs = new RoundSummary();
                rs.AtualizarDados(tentativa.Acao, tentativa.Posicao, tentativa.InimigosDerrotados, tentativa.InimigosFintados, tentativa.ContadorItensEncontrados, tentativa.ChaveNoBolso, tentativa.PontosDeVida, tentativa.PontosDeForca, tentativa.PontosDeSorte, tentativa.PocoesDeVida, tentativa.Resultado, tentativa.Ronda);
                tentativa.FinalizarRonda(rs);
            }

            return View("JogoFinalizadoAutonomo", tentativa);

        }

        [HttpGet]
        public IActionResult NovaTentativa()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovaTentativaAsync(Tentativa tentativa)
        {
            if (ModelState.IsValid)
            {
                tentativa.IniciarTentativa();
                Repositorio.AddTentativa(tentativa);

                HttpClient client = MyHttpClient.Client;
                string path = "/api/NewGame";

                NewGameRequest newGameRequest = new NewGameRequest(tentativa.Nickname, tentativa.Classe);
                string json = JsonConvert.SerializeObject(newGameRequest);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode) { return Redirect("/"); }
                string json_r = await response.Content.ReadAsStringAsync();

                GameState gs = JsonConvert.DeserializeObject<GameState>(json_r);

                tentativa.AtualizarDados(gs);


                return View("AreaDeJogo", tentativa);
            }
            else
                return View("NovaTentativa");
        }

        [HttpGet]
        public IActionResult AreaDeJogo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AreaDeJogo(int id, string button)
        {
            Tentativa tentativa = Repositorio.ObterTentativa(id);

            HttpClient client = MyHttpClient.Client;
            string path = "/api/Play";

            Playeraction acao = Playeraction.GoForward;

            switch (button)
            {
                case "Atacar":
                    acao = Playeraction.Attack;
                    break;

                case "Recuar":
                    acao = Playeraction.GoBack;
                    break;

                case "Examinar":
                    acao = Playeraction.SearchArea;
                    break;

                case "Fugir":
                    acao = Playeraction.Flee;
                    break;

                case "Poção":
                    acao = Playeraction.DrinkPotion;
                    break;

                case "Desistir":
                    {
                        acao = Playeraction.Quit;
                        tentativa.FinalizarTentativa();
                        break;
                    }
            }
            PlayRequest playRequest = new PlayRequest(id, acao);
            string json = JsonConvert.SerializeObject(playRequest);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode) { return Redirect("/"); }
            string json_r = await response.Content.ReadAsStringAsync();

            GameState gs = JsonConvert.DeserializeObject<GameState>(json_r);



            if (button != "Desistir")
            {
                tentativa.AtualizarDados(gs);
                tentativa.ExecutarAcao();
            }


            if (tentativa.JogoFinalizado)
            {
                ModeloDeHiscores modeloDeHiscores = new ModeloDeHiscores(tentativa.Nickname, tentativa.MoedasDeOuro, tentativa.Resultado, tentativa.ChaveNoBolso, tentativa.InimigosDerrotados, tentativa.InimigosFintados, tentativa.ContadorPesquisas, tentativa.ContadorItensEncontrados, tentativa.PocoesUsadas, tentativa.PocoesTotais);
                Repositorio.AddHiscores(modeloDeHiscores);
                return View("JogoFinalizado", tentativa);
            }
            else
                return View(tentativa);
        }

        public IActionResult JogoFinalizado() //para que serve esta xota? Meti debug e nunca entrou aqui xD
        {
            List<Tentativa> tentativas = Repositorio.Tentativas;
            return View(tentativas);
        }



    }
}