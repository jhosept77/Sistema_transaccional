namespace PROYECT_ONE;
using Npgsql;


public class Transacciones_cuenta
{
    public decimal Monto { get; set; }
    public DateTime Fecha_trans { get; set; }
    public string Producto_origen { get; set; }
    public string Producto_destino  { get; set; }
    public string? Descripcion  { get; set; }
    public decimal Costo_trans => Monto * 0.004m;

    public Transacciones_cuenta(decimal monto, DateTime fechaTrans, string productoOrigen , string productoDestino ,string descripcion)
    {
        this.Monto = monto;
        this.Fecha_trans = fechaTrans;
        this.Producto_origen = productoOrigen;
        this.Producto_destino = productoDestino;
        this.Descripcion = descripcion;
    }

    public Transacciones_cuenta()
    {
       Fecha_trans = DateTime.Now;
    }
    
}

public class Transacciones_cuenta_Repository
{
    private readonly string _ConnString;

    public Transacciones_cuenta_Repository(string connString)
    {
        this._ConnString = connString;
    }

    public void InsTrans_cuenta(Transacciones_cuenta trans_cun)
    {
        using var Portalbase = new NpgsqlConnection(_ConnString);
        Portalbase.Open();

        string mensaje =
            "INSERT INTO trans_cuenta(producto_origen, monto, producto_destino, fecha_trans, costo_trans, descripcion)" +
            "VALUES (@producto_origen, @monto, @producto_destino, @fecha_trans, @costo_trans, @descripcion)";
        
        var mensajero = new NpgsqlCommand(mensaje, Portalbase);
        mensajero.Parameters.AddWithValue("producto_origen", trans_cun.Producto_origen );
        mensajero.Parameters.AddWithValue("monto", trans_cun.Monto);
        mensajero.Parameters.AddWithValue("producto_destino", trans_cun.Producto_destino);
        mensajero.Parameters.AddWithValue("fecha_trans", trans_cun.Fecha_trans);
        mensajero.Parameters.AddWithValue("costo_trans", trans_cun.Costo_trans);
        mensajero.Parameters.AddWithValue("descripcion", (object?)trans_cun.Descripcion ?? DBNull.Value);
        
        mensajero.ExecuteNonQuery();
    }
} 