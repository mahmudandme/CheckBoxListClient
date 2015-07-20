using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CheckBoxListTestDemo.Models;

namespace CheckBoxListTestDemo.UI
{
   
    public partial class Home : System.Web.UI.Page
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
       

        public Home()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString);
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllUserAccessNameInDropdownList();
                ListItem li = new ListItem("Select user access", "-1");
                userAccessDropDownlist.Items.Insert(0,li);                 
            }
           
        }
        
        public void GetAllUserAccessNameInDropdownList()
        {
            userAccessDropDownlist.DataSource = GetAllUserAccessName();
            userAccessDropDownlist.DataValueField = "ID";
            userAccessDropDownlist.DataTextField = "UserAccessName";
            userAccessDropDownlist.DataBind();
        }

        protected void userAccessDropDownlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selectedUserAccessId = Convert.ToInt32(userAccessDropDownlist.SelectedValue);
           // selectedUserAccessName = userAccessDropDownlist.SelectedItem.Text;

            DeSelectItemInCheckBoxList();

            if (userAccessDropDownlist.SelectedValue=="-1")
            {
                statusLabel.Text = "Please select a user access";
            }
            else
            {
                userAccessTextBox.Text = userAccessDropDownlist.SelectedItem.Text;
                int selectedUserAccessId1 = Convert.ToInt32(userAccessDropDownlist.SelectedValue);
                List<UserRollModel> userRollModels = GetAllAccessSpecificRollsByAccessValue(selectedUserAccessId1);

                foreach (UserRollModel rollModel in userRollModels)
                {
                    foreach (ListItem itemList1 in perDealershipOverridesCheckBoxList.Items)
                    {
                        if (rollModel.Status == "True" && rollModel.RollValue == itemList1.Value)
                        {
                            itemList1.Selected = true;
                            break;
                        }
                    }
                    foreach (ListItem itemList2 in globalRulesCheckBoxList.Items)
                    {
                        if (rollModel.Status == "True" && rollModel.RollValue == itemList2.Value)
                        {
                            itemList2.Selected = true;
                            break;
                        }
                    }
                }
            }           

        }

        protected void createButton_Click(object sender, EventArgs e)
        {
            int lastIdentityOfUserAccess = 0;
            UserAccessModel userAccessModel = new UserAccessModel();
            userAccessModel.UserAccessName = userAccessTextBox.Text;
            if (userAccessTextBox.Text=="")
            {
                statusLabel.Text = "Please enter the name";
            }
            else
            {
                if (IsUserAccessNameExist(userAccessTextBox.Text))
                {
                    statusLabel.Text = "This user access name already exist. Try with another name.";
                }
                else
                {
                    if (SaveUserAccessName(userAccessModel) > 0)
                    {
                        lastIdentityOfUserAccess = GetLastIdentityOfUserAccess();

                        foreach (ListItem item in perDealershipOverridesCheckBoxList.Items)
                        {
                            UserRollModel userRollModel = new UserRollModel();
                            userRollModel.RollValue = item.Value;
                            if (item.Selected)
                            {
                                userRollModel.Status = "True";
                            }
                            else
                            {
                                userRollModel.Status = "False";
                            }
                            userRollModel.AccessRollId = lastIdentityOfUserAccess;
                            if (SaveUserRolls(userRollModel) > 0)
                            {
                                statusLabel.ForeColor = System.Drawing.Color.Green;
                                statusLabel.Text = "User rolls saved.";
                                GetAllUserAccessNameInDropdownList();
                                ListItem li = new ListItem("Select user access", "-1");
                                userAccessDropDownlist.Items.Insert(0, li);
                            }
                            else
                            {
                                statusLabel.Text = "User rolls not saved.";
                                GetAllUserAccessNameInDropdownList();
                                ListItem li = new ListItem("Select user access", "-1");
                                userAccessDropDownlist.Items.Insert(0, li);
                            }
                        }
                        foreach (ListItem item in globalRulesCheckBoxList.Items)
                        {
                            UserRollModel userRollModel = new UserRollModel();
                            userRollModel.RollValue = item.Value;
                            if (item.Selected)
                            {
                                userRollModel.Status = "True";
                            }
                            else
                            {
                                userRollModel.Status = "False";
                            }
                            userRollModel.AccessRollId = lastIdentityOfUserAccess;
                            if (SaveUserRolls(userRollModel) > 0)
                            {
                                statusLabel.ForeColor = System.Drawing.Color.Green;
                                statusLabel.Text = "User rolls saved.";
                                GetAllUserAccessNameInDropdownList();
                                ListItem li = new ListItem("Select user access", "-1");
                                userAccessDropDownlist.Items.Insert(0, li);

                            }
                            else
                            {
                                statusLabel.Text = "User rolls not saved.";
                                GetAllUserAccessNameInDropdownList();
                                ListItem li = new ListItem("Select user access", "-1");
                                userAccessDropDownlist.Items.Insert(0, li);
                            }

                        }
                        DeSelectItemInCheckBoxList();
                    }
                    else
                    {
                        statusLabel.Text = "User Access not saved.";
                    }
                }                              
            }                           
        }

        public void DeSelectItemInCheckBoxList()
        {
            foreach (ListItem item in perDealershipOverridesCheckBoxList.Items)
            {
                item.Selected = false;
            }
            foreach (ListItem item in globalRulesCheckBoxList.Items)
            {
                item.Selected = false;
            }
        }


        public bool IsUserAccessNameExist(string userAccessNameFromTextBox)
        {
            bool isUserAccessExist = false;
            string query = String.Format("Select * from tblUserAccess where UserAccessName='{0}'", userAccessNameFromTextBox);
            sqlCommand.CommandText = query;
            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader rdr = sqlCommand.ExecuteReader();
                while (rdr.Read())
                {
                    isUserAccessExist = true;
                }
                sqlConnection.Close();
            }
            return isUserAccessExist;
        }

        public int SaveUserAccessName(UserAccessModel userAccessModel)
        {
            int rowsInserted = 0;
            string query = String.Format("Insert into tblUserAccess values('{0}')",userAccessModel.UserAccessName);
           
            using (SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString))
            {
                SqlCommand cmd4 = new SqlCommand(query,con4);
                con4.Open();
                rowsInserted = cmd4.ExecuteNonQuery();
                con4.Close();
            }
            return rowsInserted;
        }

        public int GetLastIdentityOfUserAccess()
        {
            int lastIdentityValue = 0;
            string query = String.Format("Select IDENT_CURRENT('tblUserAccess')");
            
            using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString))
            {
                SqlCommand cmd1 = new SqlCommand(query,con1);
                con1.Open();
                lastIdentityValue = Convert.ToInt32(cmd1.ExecuteScalar());
                con1.Close();
            }
            return lastIdentityValue;
        }

       
        public int SaveUserRolls(UserRollModel userRollModel)
        {
            int rowsInserted = 0;
            
            string query = String.Format("Insert into tblUserRoll values('{0}','{1}','{2}')",userRollModel.RollValue,userRollModel.Status,userRollModel.AccessRollId);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query,con);
                con.Open();
                rowsInserted = cmd.ExecuteNonQuery();
                con.Close();

            }
            return rowsInserted;
        }

        public List<UserAccessModel> GetAllUserAccessName()
        {
            List<UserAccessModel> userAccessModels = new List<UserAccessModel>();
            string query = String.Format("Select * from tblUserAccess");
           // sqlCommand.CommandText = query;
            using (SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query,con3);
                con3.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    UserAccessModel userAccessModel = new UserAccessModel();
                    userAccessModel.ID = Convert.ToInt32(rdr[0]);
                    userAccessModel.UserAccessName = rdr[1].ToString();
                    userAccessModels.Add(userAccessModel);
                }
                con3.Close();
            }
            return userAccessModels;
        }


        public List<UserRollModel> GetAllAccessSpecificRollsByAccessValue(int userAccessNameId)
        {
            List<UserRollModel> userRollModels = new List<UserRollModel>();
            string query = String.Format("Select UserRollValue,RollStatus from tblUserRoll where UserAccessNameId='{0}' order by UserRollValue ", userAccessNameId);
            sqlCommand.CommandText = query;
            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader rdr = sqlCommand.ExecuteReader();
                while (rdr.Read())
                {
                    UserRollModel userRollModel = new UserRollModel();
                    userRollModel.RollValue = rdr[0].ToString();
                    userRollModel.Status = rdr[1].ToString();
                    userRollModels.Add(userRollModel);
                }
                sqlConnection.Close();                
            }
            return userRollModels;
        }

       

        public int UpdateSelectedUserAccessNameAndGetId(UserAccessModel userAccessModel)
        {
            int updatedId = 0;
            string query = String.Format("Update tblUserAccess set UserAccessName='{0}' output inserted.ID where ID='{1}'", userAccessModel.UserAccessName,userAccessModel.ID);

            using (SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString))
            {
                SqlCommand cmd5 = new SqlCommand(query,con5);
                con5.Open();
                updatedId = Convert.ToInt32(cmd5.ExecuteScalar());
                con5.Close();
            }
            return updatedId;

        }

        public int UpdateRoolsOfSelectedUserAccess(UserRollModel userRollModel)
        {
            int rowsUpdated = 0;
            string query = String.Format("Update tblUserRoll set RollStatus='{0}' where UserAccessNameId='{1}'", userRollModel.Status,userRollModel.AccessRollId);
            using (SqlConnection con7 = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString) )
            {
                SqlCommand cmd7 = new SqlCommand(query,con7);
                con7.Open();
                rowsUpdated = cmd7.ExecuteNonQuery();
                con7.Close();
            }
            return rowsUpdated;
        }
        protected void updateButton_Click(object sender, EventArgs e)
        {
            if (userAccessDropDownlist.SelectedValue == "-1")
            {
                statusLabel.Text = "Please select user access name";
            }
            else
            {
                UserAccessModel userAccessModel = new UserAccessModel();
                userAccessModel.ID = Convert.ToInt32(userAccessDropDownlist.SelectedValue);
                if (userAccessTextBox.Text=="")
                {
                    statusLabel.Text = "Please enter the value in the textbox.";
                }
                else
                {
                    userAccessModel.UserAccessName = userAccessTextBox.Text;
                }

                if (UpdateSelectedUserAccessNameAndGetId(userAccessModel) > 0)
                {
                    int rowsUpdated = 0;
                    int userAccessId = UpdateSelectedUserAccessNameAndGetId(userAccessModel);
                    string query = String.Format("Update tblUserRoll set RollStatus=@rollStatus where UserRollValue=@userRollValue and UserAccessNameId=@userAccessId");
                    using (SqlConnection con8 = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString))
                    {
                        SqlCommand cmd8 = new SqlCommand(query,con8);
                        con8.Open();
                        foreach (ListItem item in perDealershipOverridesCheckBoxList.Items)
                        {
                            cmd8.Parameters.Clear();
                            if (item.Selected)
                            {
                                cmd8.Parameters.AddWithValue("@rollStatus","True"); 
                            }
                            else
                            {
                                cmd8.Parameters.AddWithValue("@rollStatus","False");
                            }
                            
                            cmd8.Parameters.AddWithValue("@userRollValue",item.Value);
                            cmd8.Parameters.AddWithValue("@userAccessId", userAccessId);
                            rowsUpdated = cmd8.ExecuteNonQuery();

                        }
                        foreach (ListItem item in globalRulesCheckBoxList.Items)
                        {
                            cmd8.Parameters.Clear();


                            if (item.Selected)
                            {
                                cmd8.Parameters.AddWithValue("@rollStatus", "True");
                            }
                            else
                            {
                                cmd8.Parameters.AddWithValue("@rollStatus", "False");
                            }
                            
                            cmd8.Parameters.AddWithValue("@userRollValue", item.Value);
                            cmd8.Parameters.AddWithValue("@userAccessId", userAccessId);
                            rowsUpdated= cmd8.ExecuteNonQuery();

                        }
                        
                        con8.Close();
                        if (rowsUpdated>0)
                        {
                            statusLabel.ForeColor = System.Drawing.Color.Green;
                            statusLabel.Text = "Updated.";
                            DeSelectItemInCheckBoxList();
                            GetAllUserAccessNameInDropdownList();
                            ListItem li = new ListItem("Select user access", "-1");
                            userAccessDropDownlist.Items.Insert(0, li);
                            userAccessTextBox.Text = "";

                        }
                        else
                        {
                            statusLabel.Text = "Not Updated.";
                        }
                    }  
                 
                }
            }
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            if (userAccessTextBox.Text=="")
            {
                statusLabel.Text = "Please Enter the access name.";
            }
            else
            {
                if (! IsUserAccessNameExist(userAccessTextBox.Text))
                {
                    statusLabel.Text = "This access name not exist.";
                }
                else
                {
                    if (DeleteAccessName(userAccessTextBox.Text)>0)
                    {
                        statusLabel.ForeColor = System.Drawing.Color.Red;
                        statusLabel.Text = "User access deleted";
                        DeSelectItemInCheckBoxList();
                        GetAllUserAccessNameInDropdownList();
                        ListItem li = new ListItem("Select user access", "-1");
                        userAccessDropDownlist.Items.Insert(0, li);
                        userAccessTextBox.Text = "";
                    }
                    else
                    {
                        statusLabel.ForeColor = System.Drawing.Color.Red;
                        statusLabel.Text = "Not deleted.";
                    }
                }
            }
        }

        public int DeleteAccessName(string userAccessName)
        {
            int rowsDeleted = 0;
            string query = String.Format("Delete from tblUserAccess where UserAccessName='{0}'",userAccessName);
            using (SqlConnection con9 = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString))
            {
                SqlCommand cmd9 = new SqlCommand(query,con9);
                con9.Open();
                rowsDeleted = cmd9.ExecuteNonQuery();
                con9.Close();
            }
            return rowsDeleted;
        }
    }
}