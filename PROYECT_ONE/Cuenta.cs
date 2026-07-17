namespace PROYECT_ONE;
using System;
using System.Security.Cryptography;
using Npgsql;

public class Cuenta
{
    public string Tipo { get; set; }
    public int Id_cliente { get; set; }
    public decimal Saldo { get; set; }
    public string No_cuenta { get; set; }
    public string Estado { get; set; }  
    public DateTime Fecha_apertura { get; set; }
    public DateTime Fecha_vencimiento { get; set; }


    public Cuenta(string tipo, int idCliente ,decimal saldo, DateTime fecha_apertura, DateTime fechaVencimiento ,string estado)
    {
        this.Tipo = tipo;
        this.Id_cliente = idCliente;
        this.Saldo = saldo;
        this.No_cuenta = GenerarNumeroCuenta();
        this.Fecha_apertura = fecha_apertura;
        this.Fecha_vencimiento = fechaVencimiento;
        this.Estado = estado;
    }

    public Cuenta(string tipo, int idCliente) : this(tipo, idCliente,0, DateTime.Now, DateTime.Now.AddYears(3), "ACTIVO")
    {
    }

    private string GenerarNumeroCuenta()
    {
        byte[] bytes = new byte[16];
        RandomNumberGenerator.Fill(bytes);

        string numero = "";
        foreach (byte b in bytes)
        {
            numero  += (b % 10) .ToString();
        }
        
        return numero;
    }
    
}

public class CuentaRepository
{
    private readonly string _ConnString;

    public CuentaRepository(string connString) 
    {
        this._ConnString = connString;
    }

    public int InsertarCuenta(Cuenta cunn)
    {
        using var puerta = new NpgsqlConnection(_ConnString);
        puerta.Open();

        var banco =
            "INSERT INTO cuenta (tipo, id_cuenta ,saldo, no_cuenta, fecha_apertura, estado, fecha_vencimiento)" +
            "VALUES (@tipo, @id_cuenta, @saldo, @no_cuenta, @fecha_apertura, @estado, @fecha_vencimiento)" +
            "RETURNING id_cuenta";

        using var puertafinal = new  NpgsqlCommand(banco, puerta);
        puertafinal.Parameters.AddWithValue("tipo", cunn.Tipo );
        puertafinal.Parameters.AddWithValue("id_cliente", cunn.Id_cliente);
        puertafinal.Parameters.AddWithValue("saldo", cunn.Saldo);
        puertafinal.Parameters.AddWithValue("no_cuenta", cunn.No_cuenta);
        puertafinal.Parameters.AddWithValue("fecha_apertura", cunn.Fecha_apertura);
        puertafinal.Parameters.AddWithValue("fecha_vencimiento", cunn.Fecha_vencimiento);
        puertafinal.Parameters.AddWithValue("estado", cunn.Estado);

        object idretorno = puertafinal.ExecuteScalar();
        int No_cuenta_id = (int)idretorno;
        return No_cuenta_id;
    }
    
}


