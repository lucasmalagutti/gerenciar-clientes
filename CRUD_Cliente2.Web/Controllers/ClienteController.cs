using CRUD_Cliente2.Web.Facade;
using CRUD_Cliente2.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers;

public class ClienteController : Controller
{
    private readonly ClienteFacade _clienteFacade;

    public ClienteController(ClienteFacade clienteFacade)
    {
        _clienteFacade = clienteFacade;
    }
    public async Task<IActionResult> Index(string filtro)
    {
        var clientes = await _clienteFacade.ConsultarClientesAsync(filtro);
        var viewModel = clientes.Select(c => new ClienteIndexViewModel
        {
            Id = c.Id,
            Nome = c.Nome,
            CPF = c.CPF,
            Email = c.Email,
            Telefone = $"({c.TelefoneDDD}) {c.TelefoneNumero}",
            Ativo = c.Ativo,
            Ranking = c.Ranking
        }).ToList();

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var cliente = await _clienteFacade.ObterPorIdAsync(id);
        if (cliente == null)
            return NotFound();

        var viewModel = new ClienteDetailsViewModel(cliente);
        return View(viewModel);
    }

    public IActionResult Create()
    {
        var viewModel = new ClienteCreateViewModel
        {
            EnderecoResidencial = new EnderecoViewModel(),
            EnderecoCobranca = new EnderecoViewModel()
        };
        _clienteFacade.PopularDropdowns(viewModel.EnderecoResidencial);
        _clienteFacade.PopularDropdowns(viewModel.EnderecoCobranca);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClienteCreateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var cliente = viewModel;
                await _clienteFacade.CadastrarClienteAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var cliente = await _clienteFacade.ObterPorIdAsync(id);
        if (cliente == null)
            return NotFound();

        var viewModel = ClienteEditViewModel.FromEntity(cliente);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ClienteEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var cliente = viewModel.ToEntity();
        await _clienteFacade.EditarClienteAsync(cliente);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> AlterarSenha(int id)
    {
        var cliente = await _clienteFacade.ObterPorIdAsync(id);
        if (cliente == null)
            return NotFound();

        return View(new ClienteSenhaViewModel { ClienteId = id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AlterarSenha(ClienteSenhaViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        await _clienteFacade.AlterarSenhaAsync(viewModel.ClienteId, viewModel.NovaSenha);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Inativar(int id)
    {
        await _clienteFacade.InativarClienteAsync(id);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult AdicionarEndereco(int clienteId)
    {
        var viewModel = new EnderecoViewModel { ClienteId = clienteId };
        _clienteFacade.PopularDropdowns(viewModel);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AdicionarEndereco(EnderecoViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            _clienteFacade.PopularDropdowns(viewModel);
            return View(viewModel);
        }

        var endereco = viewModel.ToEntity();
        await _clienteFacade.AdicionarEnderecoAsync(viewModel.ClienteId, endereco);

        return RedirectToAction("Details", new { id = viewModel.ClienteId });
    }
    public IActionResult AdicionarCartao(int clienteId)
    {
        var viewModel = new CartaoViewModel
        {
            ClienteId = clienteId
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AdicionarCartao(CartaoViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        try
        {
            await _clienteFacade.AdicionarCartaoAsync(viewModel);
            return RedirectToAction(nameof(Details), new { id = viewModel.ClienteId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(viewModel);
        }
    }
}
