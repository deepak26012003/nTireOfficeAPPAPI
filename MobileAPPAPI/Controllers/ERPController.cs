﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ERPController : ControllerBase
    {
        public static Helper objhelper = new Helper();
        public static string strconn = objhelper.Connectionstring();
        static string JsonString = string.Empty;
        //node source starts


        [HttpPost]
        [Route("list_po_order")]
        public async Task<ActionResult<ERP>> list_po_order(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                DataSet dsuserdetails = new DataSet();
                dbConn.Open();
                string sql = "ERP_PO_ORDER_SUMMARY";
                SqlCommand cmd = new SqlCommand(sql, dbConn);


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BRANCHID", data.branchid);
                cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                cmd.Parameters.AddWithValue("@PONUMBER", data.ponumber);
                cmd.Parameters.AddWithValue("@VENDORCODE", data.vendorcode);
                cmd.Parameters.AddWithValue("@FROMDATE", data.fromdate);
                cmd.Parameters.AddWithValue("@TODATE", data.todate);
                cmd.Parameters.AddWithValue("@STATUS", data.status);
                cmd.Parameters.AddWithValue("@ITEMCODE", data.itemcode);
                cmd.Parameters.AddWithValue("@usertype", data.usertype);
                cmd.Parameters.AddWithValue("@userid", data.userid);
                cmd.Parameters.AddWithValue("@PAGEINDEX", data.pageindex);
                cmd.Parameters.AddWithValue("@PAGESIZE", data.pagesize);
                cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.sortexpression);
                cmd.Parameters.AddWithValue("@ALPHANAME", data.alphaname);
                cmd.Parameters.AddWithValue("@PRSCODE", data.prscode);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                    logdata = DataTableToJSONWithStringBuilder(results);

                    dbConn.Close();
                }
                var result = (new { logdata });
                return Ok(Logdata1);
            }
        }







        [HttpPost]
        [Route("listvendor_items")]
        public async Task<ActionResult<ERP>> listvendor_items(ERP data)
        {
           // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select Vendor_Code,vendor_id,Vendor_Name from ERP_VENDOR_MASTER where status='A'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);
            }
        }





        [HttpPost]
        [Route("getAllBranches")]
        public async Task<ActionResult<ERP>> getAllBranches(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select BRANCH_CODE, BRANCH_ID  from BO_BRANCH_MASTER where STATUS='A'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                    logdata = DataTableToJSONWithStringBuilder(results);

                    dbConn.Close();
                }
                var result = (new { recordsets = logdata });
                return Ok(Logdata1);
            }
        }




        [HttpPost]
        [Route("vendor_detail")]
        public async Task<ActionResult<ERP>> vendor_detail(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from PROCUREMENTVENDORMASTER where Code='" + data.code+"'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                //for (int i = 0; i < results.Rows.Count; i++)
                //{
                //    DataRow row = results.Rows[i];
                //    Logdata1 = DataTableToJSONWithStringBuilder(results);
                //    logdata = DataTableToJSONWithStringBuilder(results);

                //    dbConn.Close();
                //}
                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);

           
            }
        }



        [HttpPost]
        [Route("vendors_quotations")]
        public async Task<ActionResult<ERP>> vendors_quotations(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                DataSet dsuserdetails = new DataSet();
                dbConn.Open();
                string sql = "ERP_VENDORQUOTATION_SUMMARY";
                SqlCommand cmd = new SqlCommand(sql, dbConn);


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                cmd.Parameters.AddWithValue("@BRANCHID", data.branchid);
                cmd.Parameters.AddWithValue("@RFQCODE", data.rfqcode);
                cmd.Parameters.AddWithValue("@FROMDATE", "");
                cmd.Parameters.AddWithValue("@TODATE", "");
                cmd.Parameters.AddWithValue("@ITEMCODE", data.itemcode);
                cmd.Parameters.AddWithValue("@VENDORID", "");
                cmd.Parameters.AddWithValue("@QUOTEREF", "");
                cmd.Parameters.AddWithValue("@STATUS", 0);
                cmd.Parameters.AddWithValue("@PAGEINDEX", 0);
                cmd.Parameters.AddWithValue("@PAGESIZE", 500);
                cmd.Parameters.AddWithValue("@SORTEXPRESSION", "item_id");
                cmd.Parameters.AddWithValue("@ALPHANAME", "");
                cmd.Parameters.AddWithValue("@MODE", 2);
                cmd.Parameters.AddWithValue("@VENDORCODE", "");
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                    logdata = DataTableToJSONWithStringBuilder(results);

                    dbConn.Close();
                }
                var result = (new { logdata });
                return Ok(Logdata1);
            }
        }






        [HttpPost]
        [Route("get_vendorid")]
        public async Task<ActionResult<ERP>> get_vendorid(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            string vendorid = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                DataSet dsuserdetails = new DataSet();
                dbConn.Open();
                string sql = "ERP_VALIDATE_VENDORID";
                SqlCommand cmd = new SqlCommand(sql, dbConn);


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FUNCTIOND", data.FUNCTION_ID);
                cmd.Parameters.AddWithValue("@USERCODE", data.usercode);
               
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    vendorid =row["vendor_id"].ToString();
                 
                }

                string query = "";
                query = "select * from ERP_VENDOR_MASTER where vendor_id='" + vendorid + "'";

                SqlCommand cmd1 = new SqlCommand(query, dbConn);
                var reader1 = cmd.ExecuteReader();
                System.Data.DataTable results1 = new System.Data.DataTable();
                results1.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

               
                var result = (new { logdata });
                return Ok(Logdata1);
            }
        }



        [HttpPost]
        [Route("vendor_category")]
        public async Task<ActionResult<ERP>> vendor_category(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from BO_PARAMETER where TYPE = 'VendorCategory' and Status = 'A'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);


            }
        }




        [HttpPost]
        [Route("company_category")]
        public async Task<ActionResult<ERP>> company_category(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from BO_PARAMETER where TYPE = 'PROC_CompanyCategory' and Status = 'A'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);


            }
        }


        [HttpPost]
        [Route("company_type")]
        public async Task<ActionResult<ERP>> company_type(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from BO_PARAMETER where TYPE = 'BusinessType' and Status = 'A'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);


            }
        }


        [HttpPost]
        [Route("vendor_country")]
        public async Task<ActionResult<ERP>> vendor_country(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from BO_PARAMETER where TYPE = 'PROC_Country' and Status = 'A'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);


            }
        }


        [HttpPost]
        [Route("vendor_region")]
        public async Task<ActionResult<ERP>> vendor_region(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "SELECT region_desc AS TEXT,region_id AS VALUE FROM BO_REGION_MASTER WITH(NOLOCK) WHERE STATUS='A'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);


            }
        }



        [HttpPost]
        [Route("list_vendors_items")]
        public async Task<ActionResult<ERP>> list_vendors_items(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select vendor_id from ERP_VENDOR_MASTER where Vendor_Code='" + data.vendorcode+"'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);


            }
        }

        //node source end



        //new method starts

        //PRS search

        //PRS search
        [HttpPost]
        [Route("get_PRS_search")]
        public async Task<ActionResult<ERP>> get_PRS_search(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
        
            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {


                    if (data.functionid.ToString() == "0" || data.functionid.ToString() == "" || data.functionid.ToString() == string.Empty || data.functionid.ToString() == null)
                    {
                        data.functionid = "0";
                    }
                    if (data.branchid.ToString() == "0" || data.branchid.ToString() == "" || data.branchid.ToString() == string.Empty || data.branchid.ToString() == null)
                    {
                        data.branchid = "0";
                    }
                    if (data.prscode.ToString() == "0" || data.prscode.ToString() == "" || data.prscode.ToString() == string.Empty || data.prscode.ToString() == null)
                    {
                        data.prscode = "0";
                    }
                    if (data.fromdate.ToString() == "0" || data.fromdate.ToString() == "" || data.fromdate.ToString() == string.Empty || data.fromdate.ToString() == null)
                    {
                        data.fromdate = "0";
                    }
                    if (data.todate.ToString() == "0" || data.todate.ToString() == "" || data.todate.ToString() == string.Empty || data.todate.ToString() == null)
                    {
                        data.todate = "0";
                    }
                    if (data.reuestdate.ToString() == "0" || data.reuestdate.ToString() == "" || data.reuestdate.ToString() == string.Empty || data.reuestdate.ToString() == null)
                    {
                        data.reuestdate = "0";
                    }
                    if (data.status.ToString() == "0" || data.status.ToString() == "" || data.status.ToString() == string.Empty || data.status.ToString() == null)
                    {
                        data.status = "0";
                    }
                    if (data.currentstatus.ToString() == "0" || data.currentstatus.ToString() == "" || data.currentstatus.ToString() == string.Empty || data.currentstatus.ToString() == null)
                    {
                        data.currentstatus = "0";
                    }
                    if (data.reqtype.ToString() == "0" || data.reqtype.ToString() == "" || data.reqtype.ToString() == string.Empty || data.reqtype.ToString() == null)
                    {
                        data.reqtype = "0";
                    }
                    if (data.requser.ToString() == "0" || data.requser.ToString() == "" || data.requser.ToString() == string.Empty || data.requser.ToString() == null)
                    {
                        data.requser = "0";
                    }
                    if (data.usertype.ToString() == "0" || data.usertype.ToString() == "" || data.usertype.ToString() == string.Empty || data.usertype.ToString() == null)
                    {
                        data.usertype = "0";
                    }
                    if (data.alphaname.ToString() == "0" || data.alphaname.ToString() == "" || data.alphaname.ToString() == string.Empty || data.alphaname.ToString() == null)
                    {
                        data.alphaname = "0";
                    }
                    if (data.qutype.ToString() == "0" || data.qutype.ToString() == "" || data.qutype.ToString() == string.Empty || data.qutype.ToString() == null)
                    {
                        data.qutype = "0";
                    }
                    if (data.prsref.ToString() == "0" || data.prsref.ToString() == "" || data.prsref.ToString() == string.Empty || data.prsref.ToString() == null)
                    {
                        data.prsref = "0";
                    }
                    if (data.menuid.ToString() == "0" || data.menuid.ToString() == "" || data.menuid.ToString() == string.Empty || data.menuid.ToString() == null)
                    {
                        data.menuid = "0";
                    }
                    if (data.userid.ToString() == "0" || data.userid.ToString() == "" || data.userid.ToString() == string.Empty || data.userid.ToString() == null)
                    {
                        data.userid = "0";
                    }


                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                    string sql = "MBL_erp_prs_getdetails";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                    cmd.Parameters.AddWithValue("@BRANCHID", data.branchid);
                    cmd.Parameters.AddWithValue("@PRSCODE", data.prscode);
                    cmd.Parameters.AddWithValue("@FROMDATE", data.fromdate);
                    cmd.Parameters.AddWithValue("@TODATE", data.todate);
                    cmd.Parameters.AddWithValue("@REQUESTDATE", data.reuestdate);
                    cmd.Parameters.AddWithValue("@STATUS", data.status);
                    cmd.Parameters.AddWithValue("@CURRENTSTATUS", data.currentstatus);
                    cmd.Parameters.AddWithValue("@REQTYPE", data.reqtype);

                    cmd.Parameters.AddWithValue("@MENUID", data.menuid);
                    cmd.Parameters.AddWithValue("@USERTYPE", data.usertype);
                    cmd.Parameters.AddWithValue("@REQUSER", data.requser);
                    cmd.Parameters.AddWithValue("@USERID", data.userid);

                    cmd.Parameters.AddWithValue("@ALPHANAME", data.alphaname);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.sortexpression);
                    cmd.Parameters.AddWithValue("@QUTYPE", data.qutype);

                    cmd.Parameters.AddWithValue("@prsref", data.prsref);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.pageindex1);
                    cmd.Parameters.AddWithValue("@PAGESIZE", data.pagesize1);
                    cmd.ExecuteNonQuery();
                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                        logdata = DataTableToJSONWithStringBuilder(results);

                        dbConn.Close();
                    }
                    var result = (new { logdata });
                    return Ok(Logdata1);
                }
            }
            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }
        }







        //PRS Insert and Update

        [HttpPost]
        [Route("get_PRS_Insert_Update")]
        public async Task<ActionResult<ERP>> get_PRS_Insert_Update(dynamic data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var stroutput = "";
            string strprscode = "";
            string strprsid = "";
            string prsid = string.Empty;
            string currency = string.Empty;
            string prs_detailID = string.Empty;
            string wfno = string.Empty;
            // var result = "";

            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {

                   

                    dbConn.Open();

                    var serializedObject = JsonConvert.SerializeObject(data).ToString();

                 

                   // var desrializedObject = JsonConvert.DeserializeObject<T>(data).ToString();
                   // var json1 = desrializedObject["Object"].ToList();
                   // var json = desrializedObject["Object"]["Itemsdetail"].ToList();

                    var obj = JsonConvert.DeserializeObject<JObject>(data.ToString());
                    var resData = obj.Value<JArray>("Itemsdetail");


                    foreach (var item in resData)
                    {
                        if (item != null)
                        {
                            string attrib1Value = item["Itemsdetail"][0]["prsid"].Value<string>();
                        }
                    }

                   


                    foreach (var item1 in obj)
                    {
                        if (item1 != null)
                        {

                            var varlist = item1["prscategory"].ToList();
                            for (int i = 0; i < item1.Count; i++)
                            {

                                string fun = item1["prscategory"]["functionid"].ToString();
                            }
                        
                        }
                    }




                    //if (resData.Count != null)
                    //{
                    //    string striii = obj["functionid"].Tostring();
                    //}

                    if (obj.prscategory == "I")
                    {



                        if (obj.prsid == "" || obj.prsid == null)
                        {

                            string query = "";
                            query = "select max(prs_id) as maxid from ERP_PRS_MASTER";

                            SqlCommand cmd = new SqlCommand(query, dbConn);
                            var reader = cmd.ExecuteReader();
                            System.Data.DataTable results = new System.Data.DataTable();
                            results.Load(reader);

                            for (int i = 0; i < results.Rows.Count; i++)
                            {
                                DataRow row = results.Rows[i];
                                strprsid = row[0].ToString() + 1;
                            }
                        }



                        if (obj.prscode == "" || obj.prscode == null)
                        {

                            DataSet dsprscode = new DataSet();
                            string sqlprscode = "ERP_PRS_ISCONFIG";
                            SqlCommand cmdprscode = new SqlCommand(sqlprscode, dbConn);


                            cmdprscode.CommandType = CommandType.StoredProcedure;

                            cmdprscode.Parameters.AddWithValue("@FUNCTIONID", obj.functionid);
                            cmdprscode.Parameters.AddWithValue("@TYPE", "Purchaserequisition");

                            cmdprscode.ExecuteNonQuery();
                            var prscodereader = cmdprscode.ExecuteReader();
                            System.Data.DataTable resultsprscode = new System.Data.DataTable();
                            resultsprscode.Load(prscodereader);
                            //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                            for (int i = 0; i < resultsprscode.Rows.Count; i++)
                            {
                                DataRow rowprscode = resultsprscode.Rows[i];
                                strprscode = rowprscode[0].ToString();


                            }


                            if (string.IsNullOrEmpty(obj.currency.ToString()))
                            {
                                if (obj.itemid.ToString() == "0")
                                {
                                    currency = obj.currency.ToString();
                                }
                                else
                                {
                                    string query = "";
                                    query = "select currency from ERP_ITEM_MASTER INNER JOIN USERACCESS WITH (NOLOCK) ON USERACCESS.FUNCTION_ID=ERP_ITEM_MASTER.FUNCTION_ID and USERACCESS.TUM_USER_ID = ERP_ITEM_MASTER.CREATED_BY where item_id='" + obj.itemid + "'";

                                    SqlCommand cmd = new SqlCommand(query, dbConn);
                                    var reader = cmd.ExecuteReader();
                                    System.Data.DataTable results = new System.Data.DataTable();
                                    results.Load(reader);

                                    for (int i = 0; i < results.Rows.Count; i++)
                                    {
                                        DataRow row = results.Rows[i];
                                        currency = row[0].ToString() + 1;
                                    }


                                }
                            }
                            else
                            {
                                currency = obj.currency.ToString();
                            }


                            DataSet dsuserdetails = new DataSet();
                            string sql = "MBL_ERP_PRS_SAVEDATA";
                            SqlCommand cmd1 = new SqlCommand(sql, dbConn);
                            cmd1.CommandType = CommandType.StoredProcedure;
                                                  
                            cmd1.Parameters.AddWithValue("@FUNCTION_ID", obj.functionid.ToString());
                            cmd1.Parameters.AddWithValue("@PRS_ID", strprsid.ToString());
                            cmd1.Parameters.AddWithValue("@CREATED_BY", obj.createdby.ToString());
                            cmd1.Parameters.AddWithValue("@REASON_PURCHASE", obj.reasonpurchase.ToString());
                            cmd1.Parameters.AddWithValue("@STATUS", obj.status.ToString());
                            cmd1.Parameters.AddWithValue("@NETAMOUNT", obj.netamount.ToString());
                            cmd1.Parameters.AddWithValue("@REQUEST_COMMENTS", obj.requestcomments.ToString());
                            cmd1.Parameters.AddWithValue("@IS_BID", obj.isbid.ToString());
                            cmd1.Parameters.AddWithValue("@PRS_TYPE", obj.prstype.ToString());
                            cmd1.Parameters.AddWithValue("@BRANCH_ID", obj.branchid.ToString());
                            cmd1.Parameters.AddWithValue("@PRS_REF", obj.prsref.ToString());
                            cmd1.Parameters.AddWithValue("@PRS_CATEGORY", obj.prscategory.ToString());
                            cmd1.Parameters.AddWithValue("@PRS_CODE", strprscode.ToString());
                            cmd1.Parameters.AddWithValue("@REQUESTED_BY", obj.requestby.ToString());
                            cmd1.Parameters.AddWithValue("@REQUESTED_DATE", obj.requestdate.ToString());
                            cmd1.Parameters.AddWithValue("@REQUEST_TYPE", obj.requettype.ToString());
                            cmd1.Parameters.AddWithValue("@CURRENCY", currency);
                            cmd1.Parameters.AddWithValue("@IPADDRESS", obj.ipaddress.ToString());
                            cmd1.Parameters.AddWithValue("@IS_SINGLE_VENDOR", obj.issinglevendor.ToString());
                            cmd1.Parameters.AddWithValue("@ORDER_PRIORITY", obj.orderpriority.ToString());
                            
                            //cmd1.ExecuteNonQuery();
                            var reader1 = cmd1.ExecuteReader();
                            System.Data.DataTable results1 = new System.Data.DataTable();
                            results1.Load(reader1);
                            //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                            for (int i = 0; i < results1.Rows.Count; i++)
                            {
                                DataRow row1 = results1.Rows[i];
                                prsid = row1[0].ToString();

                                dbConn.Close();
                            }
                            if (strprscode != string.Empty)
                            {
                                stroutput = "Inserted successfully";

                            }
                            //var result = (new { logdata });
                            //return Ok(stroutput);
                        }



                        //for (int i = 0; i < data.ERPItems.ToString().Length; i++)
                        //{
                        //    JsonString = data.ERPItems.ToString();
                        //    //sankari
                        //    var jsonobj = JsonConvert.SerializeObject(JsonString).ToString();
                        //    DataTable dt = (DataTable)JsonConvert.DeserializeObject(jsonobj, (typeof(DataTable)));
                        //    DataRow row1 = dt.Rows[i];
                        //    string itemid = row1[1].ToString();

                        //    dbConn.Close();
                        //}


                            // foreach (var item in resData)
                            //{
                            //    if (item != null)
                            //    {
                            //        string itemid = item.itemid;
                            //    }
                            //}
                        




                                    //foreach (var item in data.Itemsdetail.itemid)
                                    //{
                                    //    if (item != null)
                                    //    {
                                    //        string itemid = data.Itemsdetail.itemid;
                                    //        // string prs_id = item.prsid;
                                    //        if (itemid != null)
                                    //        {
                                    //            dbConn.Open();
                                    //            string query = "";
                                    //            query = "delete erp_prs_details where item_id = '" + itemid + "' and prs_id='" + prsid + "'";

                                    //            SqlCommand cmd = new SqlCommand(query, dbConn);
                                    //            var reader = cmd.ExecuteReader();
                                    //            System.Data.DataTable results = new System.Data.DataTable();
                                    //            results.Load(reader);
                                    //        }
                                    //    }
                                    //}






                                    //string json = data.Itemsdetail.ToString();
                                    //DataTable dt = (DataTable)JsonConvert.DeserializeObject(data.Itemsdetail.ToString(), (typeof(DataTable)));

                                    //if (prsid != null && prsid != string.Empty && prsid != "-1")
                                    //{
                                    //    foreach (var item in data.Itemsdetail)
                                    //    {

                                    //        if (item.flag.ToString() != "D")
                                    //        {
                                    //            DataSet dsuserdetails = new DataSet();
                                    //            string sql = "MBL_ERP_PRS_DETAILS_INSERT_UPDATE";
                                    //            SqlCommand objcommand1 = new SqlCommand(sql, dbConn);


                                    //            objcommand1.CommandType = CommandType.StoredProcedure;
                                    //            objcommand1.CommandType = CommandType.StoredProcedure;
                                    //            objcommand1.Parameters.AddWithValue("@FUNCTION_ID", item.function_id.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@PRS_ID", prsid);
                                    //            objcommand1.Parameters.AddWithValue("@ITEM_ID", item.itemid.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@REQUIRED_QTY", item.required_qty.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@UOM", item.UOM.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@EXPECTED_COST", item.expected_cost.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@EXP_DATE", item.exp_date.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@STATUS", item.status.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@CREATED_BY", item.created_by.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@IPADDRESS", item.ipaddress.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@UNIT_PRICE", item.unit_price.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@LIMIT", item.Limit.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@AVAILED", item.Availlimit.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@BALANCE_LIMIT", item.BalanceLimit.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@CURRENCY", currency);
                                    //            objcommand1.Parameters.AddWithValue("@CATEGORY", item.CATEGORY.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@TAX1", item.TAX1.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@TAX2", item.TAX2.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@TAX1DESC", item.TAX1DESC.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@TAX2DESC", item.TAX2DESC.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@OTHERCHARGES", item.OTHERCHARGES.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@ITEMDESC", item.item_short_desc.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@REMARKS", item.REMARKS.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@CategoryID", item.CategoryID.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@SubCategoryID", item.SubCategoryID.ToString());
                                    //            if (item.prsDetailID.ToString() != null && item.prsDetailID.ToString() != string.Empty)
                                    //                objcommand1.Parameters.AddWithValue("@prsDetailID", item.prsDetailID.ToString());
                                    //            else
                                    //                objcommand1.Parameters.AddWithValue("@prsDetailID", "0");

                                    //            objcommand1.Parameters.AddWithValue("@FreightVALUE", item.FreightVALUE.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@FreightID", item.FreightID.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@RecoveryVALUE", item.RecoveryVALUE.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@RecoveryID", item.RecoveryID.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@BDC", item.BDC.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@PTM", item.PTM.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@ACC", item.ACC.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@CPC", item.CPC.ToString());
                                    //            objcommand1.Parameters.AddWithValue("@PRS_Mode", data.PRS_Mode.ToString());
                                    //            // objcommand.Parameters.AddWithValue("@TAX3","0.00" );
                                    //            // objcommand.Parameters.AddWithValue("@TAX4","0.00");


                                    //            dbConn.Open();
                                    //            SqlDataReader dr1 = objcommand1.ExecuteReader();
                                    //            while (dr1.Read())
                                    //            {
                                    //                prs_detailID = dr1[0].ToString();
                                    //            }

                                    //        }
                                    //    }
                                    //}
                                    //if (prs_detailID != "")
                                    //{
                                    //    DataSet ds1 = new DataSet();
                                    //    string netbasecurrency = "update ERP_PRS_MASTER set netamount_baseCurr=" + data.netamount + " WHERE PRS_ID='" + prs_detailID + "' and function_id='" + data.functionid + "'";
                                    //    SqlCommand cmd = new SqlCommand(netbasecurrency, dbConn);
                                    //    var reader = cmd.ExecuteReader();
                                    //    System.Data.DataTable results = new System.Data.DataTable();
                                    //    results.Load(reader);
                                    //    if (data.release.ToString() == "true")
                                    //    {
                                    //        string wf_config_id = "select wf_config_id from BO_workflow_configurations where table_name like '%ERP_PRS_MASTER%' and status='A' and function_id='" + data.functionid + "'";
                                    //        SqlCommand cmd2 = new SqlCommand(wf_config_id, dbConn);
                                    //        var reader2 = cmd2.ExecuteReader();
                                    //        System.Data.DataTable results2 = new System.Data.DataTable();
                                    //        results2.Load(reader2);

                                    //        for (int i = 0; i < results2.Rows.Count; i++)
                                    //        {
                                    //            DataRow row = results2.Rows[i];
                                    //            wf_config_id = row[0].ToString();
                                    //        }

                                    //        // wf_config_id = objSql.getString(wf_config_id);
                                    //        if (wf_config_id != null && wf_config_id != "")
                                    //        {


                                    //            //WF_Test.Function = strFunction;
                                    //            //WF_Test.WorkFlowTable = "ERP_PRS_MASTER";
                                    //            //WF_Test.PK1 = proid;
                                    //            //WF_Test.PK2 = null;
                                    //            //WF_Test.PK3 = null;
                                    //            //WF_Test.PK4 = null;
                                    //            //WF_Test.PK5 = null;
                                    //            //WF_Test.User = strUserId;

                                    //            //WF_Test.WorkFlowinsert();
                                    //            //Btnsave.Enabled = false;
                                    //            //Btnsave.Attributes.Add("style", "background-color:#a09393");



                                    //            string wfno_sql = "select workflow_no from bo_workflow_details where module_id='260' and pk_value1='" + prs_detailID + "' ";
                                    //            SqlCommand cmd3 = new SqlCommand(wfno_sql, dbConn);
                                    //            var reader3 = cmd3.ExecuteReader();
                                    //            System.Data.DataTable results3 = new System.Data.DataTable();
                                    //            results3.Load(reader3);
                                    //            for (int i = 0; i < results3.Rows.Count; i++)
                                    //            {
                                    //                DataRow row = results3.Rows[i];
                                    //                wfno = row[0].ToString();
                                    //            }

                                    //            if (wfno != "")
                                    //            {

                                    //                string sql2 = "select *,BO_USER_MASTER.tum_user_name,ERP_ITEM_MASTER.item_short_Desc,bo_parameter.TEXT as UOMdesc,cur.code as currencydesc,ERP_PRS_MASTER.prs_code,convert(varchar, erp_prs_details.created_on, 103) as rfpdate from erp_prs_details inner join ERP_PRS_MASTER on ERP_PRS_MASTER.prs_id=erp_prs_details.prs_id  inner join BO_USER_MASTER on BO_USER_MASTER.tum_user_id=erp_prs_details.created_by and BO_USER_MASTER.FUNCTION_ID='1' inner join ERP_ITEM_MASTER on ERP_ITEM_MASTER.item_id = erp_prs_details.item_id inner join bo_parameter on bo_parameter.val = erp_prs_details.UOM and bo_parameter.type = 'UOM' and bo_parameter.FUNCTION_ID = '1' inner join bo_parameter cur on cur.val = erp_prs_details.currency and cur.type = 'currency' and cur.function_id = '1' where erp_prs_details.prs_id ='" + prs_detailID + "' ";
                                    //                SqlCommand cmd4 = new SqlCommand(sql2, dbConn);
                                    //                var reader4 = cmd4.ExecuteReader();
                                    //                System.Data.DataTable results4 = new System.Data.DataTable();
                                    //                // ds1.Load(reader4);
                                    //                results4.Load(reader4);




                                    //            }



                                    //        }
                                    //        else
                                    //        {
                                    //            string wfno_sql = "update ERP_PRS_MASTER set status='A' WHERE PRS_ID='" + prs_detailID + "' and function_id='" + data.functionid + "'";
                                    //            SqlCommand cmd3 = new SqlCommand(wfno_sql, dbConn);
                                    //            var reader3 = cmd3.ExecuteReader();
                                    //            System.Data.DataTable results3 = new System.Data.DataTable();
                                    //            results3.Load(reader3);


                                    //        }



                                    //    }
                                    //    dbConn.Close();
                                    //}









                                }
                }

                var result = (new { logdata });
                return Ok(stroutput);
            }

            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }
        }



        //PRS delete
        [HttpPost]
        [Route("get_PRS_Delete")]
        public async Task<ActionResult<ERP>> get_PRS_Delete(ERP data)
        {
            // string struser = data.user_lower;

           // List<CAMS> Logdata = new List<CAMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            DataSet DS = new DataSet();
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string sql = "MBL_ERP_PRS_DELETE";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PRSID", data.prsid);
                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                //for (int i = 0; i < results.Rows.Count; i++)
                //{
                //    DataRow row = results.Rows[i];
                //    Logdata1 = DataTableToJSONWithStringBuilder(results);
                //}

                Logdata1 = "Deleted Successfully";
                return Ok(Logdata1);

            }
        }




        //RFQ search

      
        [HttpPost]
        [Route("get_RFQ_search")]
        public async Task<ActionResult<ERP>> get_RFQ_search(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";

            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {


                    if (data.functionid.ToString() == "0" || data.functionid.ToString() == "" || data.functionid.ToString() == string.Empty || data.functionid.ToString() == null)
                    {
                        data.functionid = "0";
                    }
                   
                    if (data.prscode.ToString() == "0" || data.prscode.ToString() == "" || data.prscode.ToString() == string.Empty || data.prscode.ToString() == null)
                    {
                        data.prscode = "0";
                    }
                    if (data.itemcode.ToString() == "0" || data.itemcode.ToString() == "" || data.itemcode.ToString() == string.Empty || data.itemcode.ToString() == null)
                    {
                        data.itemcode = "0";
                    }

                    if (data.reuestdate.ToString() == "0" || data.reuestdate.ToString() == "" || data.reuestdate.ToString() == string.Empty || data.reuestdate.ToString() == null)
                    {
                        data.reuestdate = "0";
                    }


                    if (data.rfqcode.ToString() == "0" || data.rfqcode.ToString() == "" || data.rfqcode.ToString() == string.Empty || data.rfqcode.ToString() == null)
                    {
                        data.rfqcode = "0";
                    }

                    if (data.fromdate.ToString() == "0" || data.fromdate.ToString() == "" || data.fromdate.ToString() == string.Empty || data.fromdate.ToString() == null)
                    {
                        data.fromdate = "0";
                    }
                    if (data.todate.ToString() == "0" || data.todate.ToString() == "" || data.todate.ToString() == string.Empty || data.todate.ToString() == null)
                    {
                        data.todate = "0";
                    }
                    if (data.rfqfromdate.ToString() == "0" || data.rfqfromdate.ToString() == "" || data.rfqfromdate.ToString() == string.Empty || data.rfqfromdate.ToString() == null)
                    {
                        data.rfqfromdate = "0";
                    }
                    if (data.rfqtodate.ToString() == "0" || data.rfqtodate.ToString() == "" || data.rfqtodate.ToString() == string.Empty || data.rfqtodate.ToString() == null)
                    {
                        data.rfqtodate = "0";
                    }

                    if (data.status.ToString() == "0" || data.status.ToString() == "" || data.status.ToString() == string.Empty || data.status.ToString() == null)
                    {
                        data.status = "0";
                    }
                  
                    if (data.mode.ToString() == "0" || data.mode.ToString() == "" || data.mode.ToString() == string.Empty || data.mode.ToString() == null)
                    {
                        data.mode = "0";
                    }
                    if (data.pageindex1.ToString() == "0" || data.pageindex1.ToString() == "" || data.pageindex1.ToString() == string.Empty || data.pageindex1.ToString() == null)
                    {
                        data.pageindex1 = 0;
                    }
                    if (data.pagesize1.ToString() == "0" || data.pagesize1.ToString() == "" || data.pagesize1.ToString() == string.Empty || data.pagesize1.ToString() == null)
                    {
                        data.pagesize1 = 0;
                    }
                    if (data.alphaname.ToString() == "0" || data.alphaname.ToString() == "" || data.alphaname.ToString() == string.Empty || data.alphaname.ToString() == null)
                    {
                        data.alphaname = "0";
                    }
                


                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                    string sql = "MBL_erp_prs_getdetails";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                    cmd.Parameters.AddWithValue("@PRSCODE", data.prscode);
                    cmd.Parameters.AddWithValue("@ITEMCODE", data.itemcode);
                    cmd.Parameters.AddWithValue("@REQUESTED_DATE", data.reuestdate);
                    cmd.Parameters.AddWithValue("@RFQCODE", data.rfqcode);
                    cmd.Parameters.AddWithValue("@FROMDATE", data.fromdate);
                    cmd.Parameters.AddWithValue("@TODATE", data.todate);
                    cmd.Parameters.AddWithValue("@RFQFromDate", data.rfqfromdate);
                    cmd.Parameters.AddWithValue("@RFQToDate", data.rfqtodate);
                    cmd.Parameters.AddWithValue("@STATUS", data.status);
                    cmd.Parameters.AddWithValue("@MODE", data.mode);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.pageindex1);
                    cmd.Parameters.AddWithValue("@PAGESIZE", data.pagesize1);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.sortexpression);
                    cmd.Parameters.AddWithValue("@ALPHANAME", data.alphaname);

                                    
                    cmd.ExecuteNonQuery();
                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                        logdata = DataTableToJSONWithStringBuilder(results);

                        dbConn.Close();
                    }
                    var result = (new { logdata });
                    return Ok(Logdata1);
                }
            }
            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }
        }


        //get Item code auto filling details

        [HttpGet]
        [Route("getItemcode/{code}")]
        public string getItemcode(string code)
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select item_id,item_Code from ERP_ITEM_MASTER where item_code like  '%" + code + "%'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }

                dbConn.Close();

                var result = (new { recordsets = Logdata1 });

            }
            return (Logdata1);
        }


        //get Item detials

        [HttpGet]
        [Route("getItemDetail/{code}")]
        public string getItemDetail(string code)
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select * from ERP_ITEM_MASTER where item_code = '" + code + "' and function_id=1 and Status='A'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }

                dbConn.Close();

                var result = (new { recordsets = Logdata1 });

            }
            return (Logdata1);
        }


        //order priority dropdown
        [HttpGet]
        [Route("getOrderPriority")]
        public string getOrderPriority()
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select type,text,val,sequence from bo_parameter where TYPE='Order Priority' and FUNCTION_ID=1 and status='A'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }

                dbConn.Close();

                var result = (new { recordsets = Logdata1 });

            }
            return (Logdata1);
        }



        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }
    }
}
