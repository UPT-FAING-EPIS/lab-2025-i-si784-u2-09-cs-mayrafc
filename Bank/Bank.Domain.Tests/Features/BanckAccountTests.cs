using Bank.Domain;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace Bank.Domain.Tests.Features
{
    [Binding]
    public sealed class CuentaAhorroPruebas
    {
        private readonly ScenarioContext _scenarioContext;
        private CuentaAhorro? _cuenta;
        private string? _error;
        private bool _esError;

        public CuentaAhorroPruebas(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _esError = false;
        }

        [Given("la nueva cuenta numero (.*)")]
        public void DadoUnaNuevaCuenta(string numeroCuenta)
        {
            try
            {
                var cliente = Cliente.Registrar("Juan Perez");
                _cuenta = CuentaAhorro.Aperturar(numeroCuenta, cliente, 1);
            }
            catch (System.Exception ex)
            {
                _esError = true;
                _error = ex.Message;
            }
        }

        [Given("con saldo (.*)")]
        [When("deposito (.*)")]
        public void CuandoYoDeposito(decimal monto)
        {
            try
            {
                _cuenta?.Depositar(monto);
            }
            catch (System.Exception ex)
            {
                _esError = true;
                _error = ex.Message;
            }
        }

        [When("retiro (.*)")]
        public void CuandoYoRetiro(decimal monto)
        {
            try
            {
                _cuenta?.Retirar(monto);
            }
            catch (System.Exception ex)
            {
                _esError = true;
                _error = ex.Message;
            }
        }

        [Then("el saldo nuevo deberia ser (.*)")]
        public void EntoncesElSaldoNuevoDeberiaSer(decimal resultado)
        {
            Assert.That(_cuenta?.Saldo, Is.EqualTo(resultado));
        }

        [Then("deberia ser error")]
        public void EntoncesDeberiaSerError()
        {
            Assert.That(_esError, Is.True);
        }

        [Then("deberia mostrarse el error: (.*)")]
        public void EntoncesDeberiaMostrarseElError(string error)
        {
            Assert.That(_error, Is.EqualTo(error));
        }

        [When("cancelo la cuenta")]
public void CuandoCanceloLaCuenta()
{
    try
    {
        _cuenta.Cancelar();
    }
    catch (System.Exception ex)
    {
        _esError = true;
        _error = ex.Message;
    }
}

[When("intento retirar (.*)")]
public void CuandoIntentoRetirar(decimal monto)
{
    try
    {
        _cuenta.Retirar(monto);
    }
    catch (System.Exception ex)
    {
        _esError = true;
        _error = ex.Message;
    }
}

[Then("la cuenta debe estar cancelada")]
public void EntoncesCuentaDebeEstarCancelada()
{
Assert.That(_cuenta.Estado, Is.False);
}

    }
}
