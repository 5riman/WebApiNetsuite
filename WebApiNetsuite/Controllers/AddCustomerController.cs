using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiNetsuite.com.netsuite.webservices;
using WebApiNetsuite.NetSuite;

namespace WebApiNetsuite.Controllers
{
    public class AddCustomerController : ApiController
    {
        [Route("api/AddCustomer/Customers12345")]
        [HttpGet]
        public IHttpActionResult Customer()
        {
            string accountId = "TSTDRV1381304";
            string consumerKey = "62026f12ae90a4e46d042127bddd7b2112d8e1150dc337f62a6aa9f394ca6660";
            string consumerSecret = "ae81b304b22344838bb6719a09d76127c27c3803f4363292ad311eeac4d82d47";
            string tokenKey = "1eee1267c29425f12f810d0038111cd0a76c1230c70eb6e6f6a111b877559821";
            string tokenSecret = "c627d27acd107b45b7ee30700e6227d941cc3b2863d75a2dab2577dfe8df2f51";


            using (NSClient client = new NSClient(accountId, consumerKey, consumerSecret, tokenKey, tokenSecret))
            {

                Customer addcustomer = new Customer();
                addcustomer.entityId = "NS01230001111";
                addcustomer.salutation = "MR";
                addcustomer.firstName = "Pravin";
                addcustomer.lastName = "Burade";
                addcustomer.title = "Tester";
                addcustomer.companyName = "Netscore Infotech PVT";

                addcustomer.parent = new RecordRef()
                {
                    internalId = "1542"
                };

                addcustomer.entityStatus = new RecordRef()
                {
                    internalId = "13"
                };
                addcustomer.partner = new RecordRef()
                {
                    internalId = "165"
                };
                addcustomer.category = new RecordRef()
                {
                    internalId = "4"
                };

                addcustomer.defaultOrderPriority = 20;

                addcustomer.comments = "ABCDE";

                addcustomer.email = "netscoreinfotechpvt@gmail.com";

                addcustomer.altEmail = "netscoreinfotech123@gmail.com";

                addcustomer.phone = "0123456987";

                addcustomer.altPhone = "9876543210";

                addcustomer.mobilePhone = "1237894560";

                addcustomer.homePhone = "978325641";

                //addcustomer.fax = "ABCDE";

                addcustomer.subsidiary = new RecordRef()
                {
                    internalId = "1"
                };


                //priority
                StringCustomFieldRef custentity_cust_priority = new StringCustomFieldRef()
                {
                    value = "55",
                    internalId = "4565",
                    scriptId = "custentity_cust_priority"

                };
                //addcustomer.customFieldList = new CustomFieldRef[] { custentity_cust_priority };

                //ShipStation Customer Id
                StringCustomFieldRef ShipStation_Customer_Id = new StringCustomFieldRef()
                {
                    value = "ShipCust12",
                    internalId = "4790",
                    scriptId = "custentity_customer_id_station"

                };
                //addcustomer.customFieldList = new CustomFieldRef[] { ShipStation_Customer_Id };


                //ShipStation Id
                StringCustomFieldRef ShipStation_Id = new StringCustomFieldRef()
                {
                    value = "ShipCust",
                    internalId = "6082",
                    scriptId = "custentity_ss_customer_id"

                };
                //addcustomer.customFieldList = new CustomFieldRef[] { ShipStation_Id };

                //custbody_bestinet_customer_id
                StringCustomFieldRef custbody_bestinet_customer_id = new StringCustomFieldRef()
                {
                    value = "Cust12345",
                    internalId = "6383",
                    scriptId = "custentity_bestinet_customer_id"

                };
                //addcustomer.customFieldList = new CustomFieldRef[] { custbody_bestinet_customer_id };


                //QB_Customer_Id
                StringCustomFieldRef QB_Customer_Id = new StringCustomFieldRef()
                {
                    value = "QBCust12345",
                    internalId = "6520",
                    scriptId = "custentity_qb_customer_id"

                };
                //addcustomer.customFieldList = new CustomFieldRef[] { QB_Customer_Id };


                //Customer_Total_Order_Amountt
                StringCustomFieldRef Customer_Total_Order_Amountt = new StringCustomFieldRef()
                {
                    value = "20",
                    internalId = "7132",
                    scriptId = "custentity_cust_total_order"

                };
                //addcustomer.customFieldList = new CustomFieldRef[] { Customer_Total_Order_Amountt };

                //Address_Line_count
                StringCustomFieldRef Address_Line_count = new StringCustomFieldRef()
                {
                    value = "5",
                    internalId = "Address_Line_count",
                    scriptId = "custentity_address_line_count"

                };
                //addcustomer.customFieldList = new CustomFieldRef[] { Address_Line_count };


                //customer_brand
                ListOrRecordRef customer_brand = new ListOrRecordRef();
                customer_brand.internalId = "10";
                SelectCustomFieldRef Customer_brand = new SelectCustomFieldRef()
                {
                    value = customer_brand,
                    scriptId = "custentity_dmm_customerbrand"
                };
                //addcustomer.customFieldList = new CustomFieldRef[] { Customer_brand };


                //Email_Address_for_Payment_Notification
                StringCustomFieldRef Email_Address_for_Payment_Notification = new StringCustomFieldRef()
                {
                    value = "5",
                    internalId = "Address_Line_count",
                    scriptId = "custentity_address_line_count"

                };
                //addcustomer.customFieldList = new CustomFieldRef[] { Email_Address_for_Payment_Notification };


                //Total_SO_Amount_Not_Billed
                StringCustomFieldRef Total_SO_Amount_Not_Billed = new StringCustomFieldRef()
                {
                    value = "10",
                    internalId = "9104",
                    scriptId = "custentity26"

                };
                //addcustomer.customFieldList = new CustomFieldRef[] { Total_SO_Amount_Not_Billed };

                //Transactions_Need_Approval
                BooleanCustomFieldRef Transactions_Need_Approval = new BooleanCustomFieldRef()
                {
                    value = false,
                    internalId = "9220",
                    scriptId = "custentity_naw_trans_need_approval"
                };
                //addcustomer.customFieldList = new CustomFieldRef[] { Transactions_Need_Approval };


                //Customer_catagiry
                ListOrRecordRef XYZ = new ListOrRecordRef();
                XYZ.internalId = "1";
                SelectCustomFieldRef Customer_catagiry = new SelectCustomFieldRef()
                {
                    value = XYZ,
                    internalId = "9704",
                    scriptId = "custentity28"
                };
                //addcustomer.customFieldList = new CustomFieldRef[] { Customer_catagiry };


                //Custom_Entity
                ListOrRecordRef ABC = new ListOrRecordRef();
                ABC.internalId = "1";
                SelectCustomFieldRef Custom_Entity = new SelectCustomFieldRef()
                {
                    value = ABC,
                    internalId = "9714",
                    scriptId = "custentitycus_entity"
                };
                //addcustomer.customFieldList = new CustomFieldRef[] { Custom_Entity };

                //CUSTOMER_ID
                StringCustomFieldRef CUSTOMER_ID = new StringCustomFieldRef()
                {
                    value = "Customer123",
                    internalId = "10740",
                    scriptId = "custentity_ipass_customerid"

                };
                //addcustomer.customFieldList = new CustomFieldRef[] { CUSTOMER_ID };

                List<CustomFieldRef> myCustFields = new List<CustomFieldRef>();
                myCustFields.Add(custentity_cust_priority);
                myCustFields.Add(ShipStation_Customer_Id);
                myCustFields.Add(ShipStation_Id);
                myCustFields.Add(custbody_bestinet_customer_id);
                myCustFields.Add(QB_Customer_Id);
                myCustFields.Add(Customer_Total_Order_Amountt);
                myCustFields.Add(Address_Line_count);
                myCustFields.Add(Customer_brand);
                myCustFields.Add(Email_Address_for_Payment_Notification);
                myCustFields.Add(Total_SO_Amount_Not_Billed);
                myCustFields.Add(Transactions_Need_Approval);
                myCustFields.Add(Customer_catagiry);
                myCustFields.Add(Custom_Entity);
                myCustFields.Add(CUSTOMER_ID);

                addcustomer.customFieldList = myCustFields.ToArray();




                // Add billing address
                Address billingAddress = new Address();
                billingAddress.addr1 = "Billing Address Line 1";
                billingAddress.addr2 = "Kondapur";
                billingAddress.city = "Hyderabad";
                billingAddress.state = "Telangana";
                billingAddress.zip = "789456";
                billingAddress.addrPhone = "9876543210";
                billingAddress.country = Country._india;
                // Set other billing address fields as needed
                CustomerAddressbook billingCustomerAddress = new CustomerAddressbook();
                billingCustomerAddress.defaultBilling = true;
                billingCustomerAddress.defaultBillingSpecified = true;
                billingCustomerAddress.defaultShipping = false;
                billingCustomerAddress.defaultShippingSpecified = false;
                billingCustomerAddress.addressbookAddress = billingAddress;


                // Add shipping address
                Address shippingAddress = new Address();
                shippingAddress.addr1 = "Shipping Address Line 1";
                shippingAddress.addr2 = "KDK Square";
                shippingAddress.city = "Nagpur";
                shippingAddress.state = "Maharashtra";
                shippingAddress.zip = "441909";
                shippingAddress.addrPhone = "1234567890";
                shippingAddress.country = Country._india;
                // Set other shipping address fields as needed
                CustomerAddressbook shippingCustomerAddress = new CustomerAddressbook();
                shippingCustomerAddress.defaultShipping = true;
                shippingCustomerAddress.defaultShippingSpecified = true;
                shippingCustomerAddress.defaultBilling = false; // Set to false as this is not a billing address
                shippingCustomerAddress.defaultBillingSpecified = false;
                shippingCustomerAddress.addressbookAddress = shippingAddress;

                // Add the shipping address entry to the customer record
                addcustomer.addressbookList = new CustomerAddressbookList();

                var list = new List<CustomerAddressbook>();
                list.Add(shippingCustomerAddress);
                list.Add(billingCustomerAddress);
                addcustomer.addressbookList.addressbook = list.ToArray();




                //Shipping Address
                //CustomerAddressbook address = new CustomerAddressbook();
                //address.defaultShipping = true;
                //address.defaultShippingSpecified = true;
                //address.label = "Shipping Address";

                //address.addressbookAddress = new Address()
                //{
                //    addr1 = "Kondapur",
                //    city = "Hyderabad",
                //    state = "Teengana",
                //    zip = "449019",
                //    country = Country._india

                //};
                //CustomerAddressbookList addressList = new CustomerAddressbookList();
                //CustomerAddressbook[] addresses = new CustomerAddressbook[1];
                //addresses[0] = address;
                //addressList.addressbook = addresses;
                //addcustomer.addressbookList = addressList;


                //Billing Address
                //CustomerAddressbook address1 = new CustomerAddressbook();
                //address1.defaultBilling = true;
                //address1.defaultBillingSpecified = true;
                //address1.label = "Billing Address";

                //address1.addressbookAddress = new Address()
                //{
                //    addr1 = "KDK Square",
                //    city = "Nagpur",
                //    state = "Maharashtra",
                //    zip = "441909",
                //    country = Country._india

                //};
                //CustomerAddressbookList addressList1 = new CustomerAddressbookList();
                //CustomerAddressbook[] addresses1 = new CustomerAddressbook[1];
                //addresses1[0] = address1;
                //addressList.addressbook = addresses1;
                //addcustomer.addressbookList = addressList1;



                addcustomer.accountNumber = "1234567891";
                addcustomer.receivablesAccount = new RecordRef()
                {
                    internalId = "7"
                };
                addcustomer.startDate = DateTime.Now;
                addcustomer.endDate = DateTime.Now.AddDays(7);

                addcustomer.reminderDays = 10;
                addcustomer.priceLevel = new RecordRef()
                {
                    internalId = ""
                };


                addcustomer.currency = new RecordRef()
                {
                    internalId = "1"
                };
                addcustomer.terms = new RecordRef()
                {
                    internalId = "2"
                };
                addcustomer.creditLimit = 15;
                //addcustomer.creditHoldOverride = new CustomerCreditHoldOverride()
                //{

                //};


                addcustomer.taxable = true;

                addcustomer.openingBalance = 0;
                addcustomer.openingBalanceDate = DateTime.Now;
                addcustomer.thirdPartyAcct = "03126547894";
                addcustomer.thirdPartyZipcode = "12345";

                addcustomer.openingBalanceAccount = new RecordRef()
                {
                    internalId = "7"
                };

                addcustomer.thirdPartyCountry = Country._india;



                //Add Sales in Sub tab
                CustomerSalesTeam salesTeam1 = new CustomerSalesTeam();
                salesTeam1.employee = new RecordRef()
                {
                    internalId = "1008" // Internal ID of the sales employee
                };
                salesTeam1.salesRole = new RecordRef()
                {
                    internalId = "-2" // Internal ID of the sales role
                };


                salesTeam1.isPrimary = true; // Set as primary sales item
                salesTeam1.contribution = 95;

                CustomerSalesTeamList salesTeamList = new CustomerSalesTeamList();
                salesTeamList.salesTeam = new CustomerSalesTeam[] { salesTeam1 /*, salesTeam2*/ };

                // Assign salesTeamList to the Customer object
                addcustomer.salesTeamList = salesTeamList;


                addcustomer.territory = new RecordRef()
                {
                    internalId = "10"
                };

                addcustomer.salesRep = new RecordRef()
                {
                    internalId = "1008"
                };


                //End sales subtab



                addcustomer.subsidiary = new RecordRef()
                {
                    internalId = "9"
                };
                client.SetPreferences();
                client.Service.preferences.disableMandatoryCustomFieldValidation = true;
                client.Service.preferences.disableMandatoryCustomFieldValidationSpecified = true;


                var response = client.Service.add(addcustomer);
                if (response.status.isSuccess)
                {
                    var obj = (RecordRef)response.baseRef;
                }

            }

            return null;
        }

    }
}