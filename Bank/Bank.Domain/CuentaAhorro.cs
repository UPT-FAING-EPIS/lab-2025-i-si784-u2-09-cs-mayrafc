namespace Bank.Domain
{
    public class CuentaAhorro
    {
        public const string ERROR_MONTO_MENOR_IGUAL_A_CERO = "El monto no puede ser menor o igual a 0";
        public const string ERROR_CUENTA_CANCELADA = "La cuenta está cancelada";

        public int IdCuenta { get; private set; }
        public string NumeroCuenta { get; private set; }
        public virtual Cliente Propietario { get; private set; }
        public int IdPropietario { get; private set; }
        public decimal Tasa { get; private set; }
        public decimal Saldo { get; private set; }
        public DateTime FechaApertura { get; private set; }
        public bool Estado { get; private set; } = true;

        public static CuentaAhorro Aperturar(string _numeroCuenta, Cliente _propietario, decimal _tasa)
        {
            return new CuentaAhorro()
            {
                NumeroCuenta = _numeroCuenta,
                Propietario = _propietario,
                IdPropietario = _propietario.IdCliente,
                Tasa = _tasa,
                Saldo = 0,
                Estado = true,
                FechaApertura = DateTime.Now
            };
        }

        public void Depositar(decimal monto)
        {
            if (monto <= 0)
                throw new Exception(ERROR_MONTO_MENOR_IGUAL_A_CERO);
            Saldo += monto;
        }

        public void Retirar(decimal monto)
        {
            if (!Estado)
                throw new Exception(ERROR_CUENTA_CANCELADA);
            if (monto <= 0)
                throw new Exception(ERROR_MONTO_MENOR_IGUAL_A_CERO);
            Saldo -= monto;
        }

        public void Cancelar()
        {
            Estado = false;
        }
    }
}
