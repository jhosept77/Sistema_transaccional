namespace PROYECT_ONE;
using System;
using System.Security.Cryptography;
using Npgsql;

public class Tarjeta_credito
{
    public string Tipo { get; set; }
    public double Cupo { get; set; }
    public int id_cliente { get; set; }
    public double Interes { get; set; }
    public int Max_cuotas { get; set; }
    public int Min_cuotas { get; set; }
    public DateTime Fecha_apertura { get; set; }
    public DateTime Fecha_vencimiento { get; set; }
    public string No_credito { get; set; }
    public string Estado { get; set; }
    public int Cvv { get; set; }



    public Tarjeta_credito(string tipo, double cupo, int idCliente ,double interes, int maxcuotas, int minCuotas,
        DateTime fechaApertura, DateTime fechaVencimiento, string estado)
    {
        this.Tipo = tipo;
        this.Cupo = cupo;
        this.id_cliente = idCliente;
        this.Interes = interes;
        this.Max_cuotas = maxcuotas;
        this.Min_cuotas = minCuotas;
        this.Fecha_apertura = fechaApertura;
        this.Fecha_vencimiento = fechaVencimiento;
        this.No_credito = GenerarNuneroCredito();
        this.Estado = estado;
        this.Cvv = Cvvauto();
    }

    public Tarjeta_credito()
    {
        this.No_credito = GenerarNuneroCredito();
        this.Cvv = Cvvauto();
        this.Interes = 1.70;
    }

    private string GenerarNuneroCredito()
    {
        byte[] bytes = new byte[16];
        RandomNumberGenerator.Fill(bytes);

        string numero = "";
        foreach (byte b in bytes)
        {
            numero += (b % 10).ToString();
        }

        return numero;
    }

    private int Cvvauto()
    {
        Random cvv = new Random();
        return cvv.Next(100, 999);
    }
    
}

public class Tarjeta_creditoFactory
{
    private readonly string _connString;

    public Tarjeta_creditoFactory(string connString)
    {
        _connString =  connString;
    }

    public int insertcreditcard(Tarjeta_credito tart)
    {
        using var portalbase = new NpgsqlConnection(_connString);
        portalbase.Open();

        string paquete =
            "INSERT INTO tarjeta_credito (tipo, cupo, id_cliente, interes, max_cuota, fecha_apertura, fecha_vencimineto, no_credito, estado, cvv)" +
            "VALUES (@tipo, @cupo, @id_cliente, @interes, @max_cuota, @fecha_apertura, @fecha_vencimineto, @no_credito, @estado, @cvv)" +
            "RETURNING id_tarjeta_credito";

        var mensajero = new NpgsqlCommand(paquete, portalbase);
        mensajero.Parameters.AddWithValue("tipo", tart.Tipo );
        mensajero.Parameters.AddWithValue("cupo", tart.Cupo);
        mensajero.Parameters.AddWithValue("id_cliente", tart.id_cliente);
        mensajero.Parameters.AddWithValue("interes", tart.Interes);
        
        object ss = mensajero.ExecuteScalar();
        int id = (int)ss;
        return id;
        
    }
}