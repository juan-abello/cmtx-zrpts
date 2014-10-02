using System;
﻿using System.Data.SqlClient;

//using System.Configuration;
using System.Collections;

//using System.Globalization;
using System.Web;

//using System.Web.Services;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.XPath;
using ZReports;

//using System.Threading;
using System.Collections.Generic;

namespace ZohoReportsUpdateService
{
	class MainClass
	{

		private static string connStrComertex = "server=192.168.3.252;database=comertex;uid=crm;password=Comertex14;";

		private static ArrayList getInvoices (string invoicesToExclude)
		{
			ArrayList invoices = new ArrayList ();
			SqlConnection invoiceDetail = new SqlConnection (connStrComertex);
			try {
				invoiceDetail.Open ();
			} catch (Exception e) {
				Console.WriteLine ("Error Abriendo la Conexión Con la Base de Datos: " + e.StackTrace);
			}
			SqlDataReader myReaderInvoiceDetails = null;
			string query = "SELECT \"FA_FACTU\".\"FAC_TIPO\" as 'Tipo',       \"FA_FACTU\".\"FAC_NUME\" as 'Num',        \"FA_FACTU\".\"FAC_FECH\" as 'Fecha',        \"FA_FACTU\".\"FAC_ESTA\" as 'Estado',        \"GN_ARBOS\".\"ARB_NOMB\" as 'Sucursal',        \"CA_VENDE\".\"VEN_CODI\" as 'Cod. Vendedor',        \"CA_VENDE\".\"VEN_NOMB\" as 'Nombre Vendedor',        \"FA_CLIEN\".\"CLI_FCRE\" as 'Fecha Creacion Cliente',        \"FA_CLIEN\".\"CLI_CODA\" as 'Num. Doc. Cliente',        \"FA_CLIEN\".\"CLI_NOCO\" as 'Nombre Cliente',        \"FA_TCLIE\".\"TCL_NOMB\" as 'Segmento',        UEN =         CASE \"CA_VENDE\".\"VEN_CODI\"           WHEN 2 THEN 'Negocios Especiales'           WHEN 207 THEN 'Administración'           WHEN 389 THEN 'Administración'         ELSE         CASE \"FA_TCLIE\".\"TCL_NOMB\"           WHEN 'Comercio Mayorista MP' THEN 'Mayorista'           WHEN 'Comercio Minorista MP' THEN 'Minorista'           WHEN 'Comercio PT' THEN 'Minorista'           WHEN 'Confecciones Dotaciones - MP' THEN 'Ropa de Trabajo'           WHEN 'Confecciones Femenina - MP' THEN 'Confección'           WHEN 'Confecciones Infantil - MP' THEN 'Confección'           WHEN 'Confecciones Masculina - MP' THEN 'Confección'           WHEN 'Confecciones Quirúrgica - MP' THEN 'Ropa de Trabajo'           WHEN 'Confecciones Ropa deportiva - MP' THEN 'Confección'           WHEN 'Grandes Superficies PT' THEN 'Minorista'           WHEN 'Industrial MP' THEN 'Industrial'           WHEN 'Mayoristas PT' THEN 'Mayorista'           WHEN 'Publicidad MP' THEN 'Minorista'           WHEN 'Licitaciones' THEN 'Negocios Especiales'           WHEN 'Negocios Especiales' THEN 'Negocios Especiales'           WHEN 'No Utilizar' THEN 'Administración'           WHEN 'Empleados' THEN 'Administración'           WHEN 'Fundaciones' THEN 'Administración'          ELSE 'Desconocido'        END        END,        \"IN_PRODU\".\"PRO_CODI\" as 'Codigo Producto',        \"IN_PRODU\".\"PRO_NOMB\" as 'Producto',        Cant =         CASE \"FA_FACTU\".\"FAC_TIPO\"         WHEN 'C' THEN \"FA_DFACT\".\"DFA_CANT\"*(-1)         ELSE \"FA_DFACT\".\"DFA_CANT\"         END,        \"FA_DFACT\".\"DFA_VALO\" as 'valo',        \"FA_DFACT\".\"DFA_VADE\" as 'vade',       \"FA_DFACT\".\"DMI_VALO\" as 'dmi valo',        \"FA_FACTU\".\"FAC_VATA\" as 'Tasa',        Venta =        CASE \"FA_FACTU\".\"FAC_TIPO\"         WHEN 'C' THEN ((\"DFA_CANT\"*(\"DFA_VALO\"))*\"FA_FACTU\".\"FAC_VATA\")*(-1)         ELSE (\"DFA_CANT\"*(\"DFA_VALO\"))*\"FA_FACTU\".\"FAC_VATA\"         END,         Costo_Venta =        CASE \"FA_FACTU\".\"FAC_TIPO\"         WHEN 'C' THEN (\"DFA_CANT\"*\"DMI_VALO\")*(-1)         ELSE \"DFA_CANT\"*\"DMI_VALO\"         END,         Utilidad_Bruta =       CASE \"FA_FACTU\".\"FAC_TIPO\"         WHEN 'C' THEN ((((\"DFA_CANT\"*(\"DFA_VALO\"-\"FA_DFACT\".\"DFA_VADE\"))*\"FA_FACTU\".\"FAC_VATA\") - (\"DFA_CANT\"*\"DMI_VALO\"))*(-1))-\"FA_DFACT\".\"DFA_VADE\"         ELSE (((\"DFA_CANT\"*(\"DFA_VALO\"-\"FA_DFACT\".\"DFA_VADE\"))*\"FA_FACTU\".\"FAC_VATA\") - (\"DFA_CANT\"*\"DMI_VALO\"))-\"FA_DFACT\".\"DFA_VADE\"         END,        Margen =        CASE \"FA_FACTU\".\"FAC_TIPO\"         WHEN 'C' THEN (((((\"DFA_CANT\"*(\"DFA_VALO\"-\"FA_DFACT\".\"DFA_VADE\"))*\"FA_FACTU\".\"FAC_VATA\") - (\"DFA_CANT\"*\"DMI_VALO\"))/( (\"DFA_CANT\"*(\"DFA_VALO\"-\"FA_DFACT\".\"DFA_VADE\"))*\"FA_FACTU\".\"FAC_VATA\"))*100)*(-1)         ELSE ((((\"DFA_CANT\"*(\"DFA_VALO\"-\"FA_DFACT\".\"DFA_VADE\"))*\"FA_FACTU\".\"FAC_VATA\") - (\"DFA_CANT\"*\"DMI_VALO\"))/( (\"DFA_CANT\"*(\"DFA_VALO\"-\"FA_DFACT\".\"DFA_VADE\"))*\"FA_FACTU\".\"FAC_VATA\"))*100         END   FROM   (((((\"FA_FACTU\" \"FA_FACTU\" INNER JOIN \"FA_DFACT\" \"FA_DFACT\" ON (\"FA_FACTU\".\"EMP_CODI\"=\"FA_DFACT\".\"EMP_CODI\") AND (\"FA_FACTU\".\"FAC_CONT\"=\"FA_DFACT\".\"FAC_CONT\")) INNER JOIN \"CA_VENDE\" \"CA_VENDE\" ON (\"FA_FACTU\".\"EMP_CODI\"=\"CA_VENDE\".\"EMP_CODI\") AND (\"FA_FACTU\".\"VEN_CODI\"=\"CA_VENDE\".\"VEN_CODI\")) INNER JOIN \"FA_CLIEN\" \"FA_CLIEN\" ON (\"FA_FACTU\".\"EMP_CODI\"=\"FA_CLIEN\".\"EMP_CODI\") AND (\"FA_FACTU\".\"CLI_CODI\"=\"FA_CLIEN\".\"CLI_CODI\")) INNER JOIN \"GN_ARBOL\" \"GN_ARBOS\" ON (\"CA_VENDE\".\"EMP_CODI\"=\"GN_ARBOS\".\"EMP_CODI\") AND (\"CA_VENDE\".\"SUC_CONT\"=\"GN_ARBOS\".\"ARB_CONT\")) INNER JOIN \"FA_DDISP\" \"FA_DDISP\" ON ((\"FA_DFACT\".\"EMP_CODI\"=\"FA_DDISP\".\"EMP_CODI\") AND (\"FA_DFACT\".\"FAC_CONT\"=\"FA_DDISP\".\"FAC_CONT\")) AND (\"FA_DFACT\".\"DFA_CONT\"=\"FA_DDISP\".\"DFA_CONT\")) INNER JOIN \"GN_ARBOL\" \"GN_ARBOL\" ON (\"FA_DDISP\".\"EMP_CODI\"=\"GN_ARBOL\".\"EMP_CODI\") AND (\"FA_DDISP\".\"ARB_CONT\"=\"GN_ARBOL\".\"ARB_CONT\") INNER JOIN \"FA_TCLIE\" ON \"FA_CLIEN\".\"TCL_CODI\"=\"FA_TCLIE\".\"TCL_CODI\" INNER JOIN \"IN_PRODU\" ON \"FA_DFACT\".\"PRO_CODI\"=\"IN_PRODU\".\"PRO_CODI\" WHERE  \"FA_FACTU\".\"FAC_ESTA\"='A' AND \"FA_FACTU\".\"FAC_ANOP\"=2014 AND (\"GN_ARBOL\".\"ARB_CODI\">='0' AND \"GN_ARBOL\".\"ARB_CODI\"<='ZZZZZ') AND \"GN_ARBOL\".\"TAR_CODI\"=1 AND (\"GN_ARBOS\".\"ARB_CODI\">='0' AND \"GN_ARBOS\".\"ARB_CODI\"<='ZZZZZ') AND \"GN_ARBOS\".\"TAR_CODI\"=2 AND \"FA_FACTU\".\"EMP_CODI\"=228 AND (\"FA_FACTU\".\"FAC_TIPO\"='F' OR \"FA_FACTU\".\"FAC_TIPO\"='C') AND (\"FA_FACTU\".\"TOP_CODI\"=1201 OR \"FA_FACTU\".\"TOP_CODI\"=1203 OR \"FA_FACTU\".\"TOP_CODI\"=1262 OR \"FA_FACTU\".\"TOP_CODI\"=1268 OR \"FA_FACTU\".\"TOP_CODI\"=1105 OR \"FA_FACTU\".\"TOP_CODI\"=1110 OR \"FA_FACTU\".\"TOP_CODI\"=1120 ) AND \"CA_VENDE\".\"VEN_CODI\"<>94 AND ( FA_TCLIE.TCL_CODI=30 OR FA_TCLIE.TCL_CODI=31 OR FA_TCLIE.TCL_CODI=34 OR FA_TCLIE.TCL_CODI=35 OR FA_TCLIE.TCL_CODI=36 OR FA_TCLIE.TCL_CODI=37 OR FA_TCLIE.TCL_CODI=38 OR FA_TCLIE.TCL_CODI=41 OR FA_TCLIE.TCL_CODI=42 OR FA_TCLIE.TCL_CODI=43 OR FA_TCLIE.TCL_CODI=44 OR FA_TCLIE.TCL_CODI=45 OR FA_TCLIE.TCL_CODI=46 OR FA_TCLIE.TCL_CODI=0 OR FA_TCLIE.TCL_CODI=39 OR FA_TCLIE.TCL_CODI=47 OR FA_TCLIE.TCL_CODI=48 OR FA_TCLIE.TCL_CODI=40) and (\"FA_FACTU\".\"FAC_NUME\" NOT IN (" + invoicesToExclude + ")) ORDER BY \"FA_FACTU\".\"FAC_FECH\" DESC ";
			SqlCommand myCommandInvoiceDetails = new SqlCommand (query, invoiceDetail);
			myReaderInvoiceDetails = myCommandInvoiceDetails.ExecuteReader ();
			while (myReaderInvoiceDetails.Read ()) {
				Invoice myInvoiceTmp = new Invoice (myReaderInvoiceDetails);
				invoices.Add (myInvoiceTmp);
			}
			try {
				invoiceDetail.Close ();
			} catch (Exception e) {
				Console.WriteLine ("Error Cerrando la Conexión Con la Base de Datos: " + e.StackTrace);
			}
			return invoices;
		}

		public static void Main (string[] args)
		{
			ArrayList invoicesToInsert = new ArrayList ();
			string invoicesToExclude = "89520,89319,89069,88390,87848,86693,86022,81885,73001805,73001787";
			IReportClient zReportsClient = new ReportClient (zReportsAuthToken);
			getInvoicesToExclude (zReportsClient);
			//invoicesToInsert = getInvoices (invoicesToExclude);

			//zReportsDeleteInvoiceRecords (zReportsClient, "Ventas - FARENTA");
			foreach (Invoice invoiceToInsert in invoicesToInsert) {
				//zReportsAddInvoice (zReportsClient, invoiceToInsert);
			}

		}

		private static string zReportsAuthToken = "20aefc9443fd55cacebf109b240c2fcb";



		private static bool Validator (object sender, X509Certificate certificate, X509Chain chain, 
		                               SslPolicyErrors sslPolicyErrors)
		{
			return true; //TODO quitar este método, es sólo para probar
		}

		private static Dictionary<string, string> zReportsAddInvoice (IReportClient RepClient, Invoice invoiceToInsert)
		{
			string tableURI = RepClient.GetURI ("kreyes@comertex.com.co", "Comertex", "Ventas - FARENTA");
			Dictionary<string, string> config = new Dictionary<string, string> ();
			Dictionary<string, string> ColumnValues = new Dictionary<string, string> ();
			ColumnValues.Add ("Tipo", invoiceToInsert.getTipo ());
			ColumnValues.Add ("Num", invoiceToInsert.getNum ());
			ColumnValues.Add ("Fecha", invoiceToInsert.getFecha ());
			ColumnValues.Add ("Estado", invoiceToInsert.getEstado());
			ColumnValues.Add ("Sucursal", invoiceToInsert.getSucursal ());
			ColumnValues.Add ("Cod. Vendedor", invoiceToInsert.getCodVendedor());
			ColumnValues.Add ("Nombre Vendedor", invoiceToInsert.getNombreVendedor());
			ColumnValues.Add ("Fecha Creacion Cliente", invoiceToInsert.getFechaCreacionCliente());
			ColumnValues.Add ("Num. Doc. Cliente", invoiceToInsert.getNumDocCliente());
			ColumnValues.Add ("Segmento", invoiceToInsert.getSegmento());
			ColumnValues.Add ("Nombre Cliente", invoiceToInsert.getNombreCliente());
			ColumnValues.Add ("UEN", invoiceToInsert.getUEN());
			ColumnValues.Add ("Codigo Producto", invoiceToInsert.getCodigoProducto());
			ColumnValues.Add ("Producto", invoiceToInsert.getProducto());
			ColumnValues.Add ("Cant.", invoiceToInsert.getCantidad());
			ColumnValues.Add ("valo", invoiceToInsert.getValor());
			ColumnValues.Add ("vade", invoiceToInsert.getVade());
			ColumnValues.Add ("dmi valo", invoiceToInsert.getDmiValo());
			ColumnValues.Add ("Tasa", invoiceToInsert.getTasa());
			ColumnValues.Add ("Venta", invoiceToInsert.getVenta());
			ColumnValues.Add ("Costo Venta", invoiceToInsert.getCostoVenta());
			ColumnValues.Add ("Utilidad Bruta", invoiceToInsert.getUtilidadBruta());
			ColumnValues.Add ("Margen", invoiceToInsert.getMargen());

			Dictionary<string, string> addRowRes = RepClient.AddRow (tableURI, ColumnValues, config);
			return addRowRes;
		}

		private static void zReportsDeleteInvoiceRecords (IReportClient RepClient, string table)
		{
			string tableURI = RepClient.GetURI ("kreyes@comertex.com.co", "Comertex", "Ventas - FARENTA");
			Dictionary<string, string> config = new Dictionary<string, string> ();
			RepClient.DeleteData (tableURI, "(Borrar='Si' or Borrar is null) and YEAR(Fecha)=YEAR(GETDATE())", config);
		}
		private static string getInvoicesToExclude(IReportClient RepClient){
			string tableURI = RepClient.GetURI ("kreyes@comertex.com.co", "Comertex", "TMP_CARGA");
			Dictionary<string, string> config = new Dictionary<string, string> ();
			//string retorno=RepClient.ExportDataAsJson(tableURI, "Num is not null", config);

			Dictionary<string, object> retornoDictionary=RepClient.ExportDataAsDictionary(tableURI,"Num is not null",config);
			object[] invoices;
			invoices = (object[])retornoDictionary ["rows"];
			string invoicesToExclude = "";
			for (int i = 0; i < invoices.Length; i++) {
				invoicesToExclude = invoicesToExclude + (String) (invoices [i]);
			}
			return "";
		}
	}
}





