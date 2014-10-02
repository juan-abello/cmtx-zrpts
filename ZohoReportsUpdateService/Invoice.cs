using System;
﻿using System.Data.SqlClient;

namespace ZohoReportsUpdateService
{
	public class Invoice
	{
		private string tipo = "";
		private string num	= "";
		private string fecha = "";	
		private string estado = "";	
		private string sucursal = "";	
		private string codVendedor = "";	
		private string nombreVendedor = "";	
		private string fechaCreacionCliente = "";	
		private string numDocCliente = "";	
		private string segmento = "";	
		private string nombreCliente = "";	
		private string uen = "";	
		private string codigoProducto = "";	
		private string producto = "";	
		private string cantidad = "";
		private string valor = "";	
		private string vade = "";	
		private string dmiValo = "";	
		private string tasa = "";	
		private string venta = "";	
		private string costoVenta = "";	
		private string utilidadBruta = "";
		private string margen = "";

		public Invoice ()
		{
			tipo = "";
			num	= "";
			fecha = "";	
			estado = "";	
			sucursal = "";	
			codVendedor = "";	
			nombreVendedor = "";	
			fechaCreacionCliente = "";	
			numDocCliente = "";	
			segmento = "";	
			nombreCliente = "";	
			uen = "";	
			codigoProducto = "";	
			producto = "";	
			cantidad = "";
			valor = "";	
			vade = "";	
			dmiValo = "";	
			tasa = "";	
			venta = "";	
			costoVenta = "";	
			utilidadBruta = "";
			margen = "";
		}
		public Invoice (string pTipo,
			string pNum,
			string pFecha,	
			string pEstado,	
			string pSucursal,	
			string pCodVendedor,	
			string pNombreVendedor,	
			string pFechaCreacionCliente,	
			string pNumDocCliente,	
			string pSegmento,	
			string pNombreCliente,	
			string pUen,	
			string pCodigoProducto,	
			string pProducto,	
			string pCantidad,
			string pValor,	
			string pVade,	
			string pDmiValo,	
			string pTasa,	
			string pVenta,	
			string pCostoVenta,	
			string pUtilidadBruta,
			string pMargen)
		{
			tipo = pTipo;
			num	= pNum;
			fecha = pFecha;	
			estado = pEstado;	
			sucursal = pSucursal;	
			codVendedor = pCodVendedor;	
			nombreVendedor = pNombreVendedor;	
			fechaCreacionCliente = pFechaCreacionCliente;	
			numDocCliente = pNumDocCliente;	
			segmento = pSegmento;	
			nombreCliente = pNombreCliente;	
			uen = pUen;	
			codigoProducto = pCodigoProducto;	
			producto = pProducto;	
			cantidad = pCantidad;
			valor = pValor;	
			vade = pVade;	
			dmiValo = pDmiValo;	
			tasa = pTasa;	
			venta = pVenta;	
			costoVenta = pCostoVenta;	
			utilidadBruta = pUtilidadBruta;
			margen = pMargen;
		}
		public Invoice(SqlDataReader myReaderInvoiceDetails)
		{
			tipo=myReaderInvoiceDetails ["Tipo"].ToString ();
			num=myReaderInvoiceDetails ["Num"].ToString ();
			fecha=myReaderInvoiceDetails ["Fecha"].ToString ();
			estado=myReaderInvoiceDetails ["Estado"].ToString ();
			sucursal=myReaderInvoiceDetails ["Sucursal"].ToString ();
			codVendedor=myReaderInvoiceDetails ["Cod. Vendedor"].ToString ();
			nombreVendedor=myReaderInvoiceDetails ["Nombre Vendedor"].ToString ();
			fechaCreacionCliente=myReaderInvoiceDetails ["Fecha Creacion Cliente"].ToString ();
			numDocCliente=myReaderInvoiceDetails ["Num. Doc. Cliente"].ToString ();
			segmento=myReaderInvoiceDetails ["Segmento"].ToString ();
			nombreCliente=myReaderInvoiceDetails ["Nombre Cliente"].ToString ();
			uen=myReaderInvoiceDetails ["UEN"].ToString ();
			codigoProducto=myReaderInvoiceDetails ["Codigo Producto"].ToString ();
			producto=myReaderInvoiceDetails ["Producto"].ToString ();
			cantidad=myReaderInvoiceDetails ["Cant"].ToString ();
			valor=myReaderInvoiceDetails ["valo"].ToString ();
			vade=myReaderInvoiceDetails ["vade"].ToString ();
			dmiValo=myReaderInvoiceDetails ["dmi valo"].ToString ();
			tasa=myReaderInvoiceDetails ["Tasa"].ToString ();
			venta=myReaderInvoiceDetails ["Venta"].ToString ();
			costoVenta=myReaderInvoiceDetails ["Costo_Venta"].ToString ();
			utilidadBruta=myReaderInvoiceDetails ["Utilidad_Bruta"].ToString ();
			margen=myReaderInvoiceDetails ["Margen"].ToString ();
		}
		public string getTipo(){
			return tipo;
		}
		public string getNum(){
			return num;
		}
		public string getFecha(){
			return fecha;
		}
		public string getEstado(){
			return estado;
		}
		public string getSucursal(){
			return sucursal;
		}
		public string getCodVendedor(){
			return codVendedor;
		}
		public string getNombreVendedor(){
			return nombreVendedor;
		}
		public string getFechaCreacionCliente(){
			return fechaCreacionCliente;
		}
		public string getNumDocCliente(){
			return numDocCliente;
		}
		public string getSegmento(){
			return segmento;
		}
		public string getNombreCliente(){
			return nombreCliente;
		}
		public string getUEN(){
			return uen;
		}
		public string getCodigoProducto(){
			return codigoProducto;
		}
		public string getProducto(){
			return producto;
		}
		public string getCantidad(){
			return cantidad;
		}
		public string getValor(){
			return valor;
		}
		public string getVade(){
			return vade;
		}
		public string getDmiValo(){
			return dmiValo;
		}
		public string getTasa(){
			return tasa;
		}
		public string getVenta(){
			return venta;
		}
		public string getCostoVenta(){
			return costoVenta;
		}
		public string getUtilidadBruta(){
			return utilidadBruta;
		}
		public string getMargen(){
			return margen;
		}
	}
}

