using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChatBot.Models
{
    public partial class gGMSContext : DbContext
    {

        public virtual DbSet<CmAllcode> CmAllcode { get; set; }
        public virtual DbSet<CmsContractDt> CmsContractDt { get; set; }
        public virtual DbSet<CmsContractMaster> CmsContractMaster { get; set; }
        public virtual DbSet<CmsCustomerMaster> CmsCustomerMaster { get; set; }
        //public virtual DbSet<Errors> Errors { get; set; }

        public virtual DbSet<PrdPlugin> PrdPlugin { get; set; }
        public virtual DbSet<PrdProductDt> PrdProductDt { get; set; }
        public virtual DbSet<PrdProductMaster> PrdProductMaster { get; set; }
        public virtual DbSet<PrdSource> PrdSource { get; set; }
        public virtual DbSet<PrdTemplate> PrdTemplate { get; set; }
        public virtual DbSet<SysCodemasters> SysCodemasters { get; set; }
        public virtual DbSet<SysError> SysError { get; set; }
        public virtual DbSet<SysParameters> SysParameters { get; set; }
        public virtual DbSet<SysPrefix> SysPrefix { get; set; }
        public virtual DbSet<PrdCategoryDt> PrdCategoryDt { get; set; }
        public virtual DbSet<PrdCategoryMaster> PrdCategoryMaster { get; set; }
        public virtual DbSet<CmsContractFileUpload> CmsContractFileUpload { get; set; }
        public virtual DbSet<CmsContractMasterSearch> CmsContractMasterSearchs { get; set; }
        public virtual DbSet<PrjProjectMaster> PrjProjectMaster { get; set; }
        public virtual DbSet<PrjProjectDT> PrjProjectDT { get; set; }

        public virtual DbSet<PrdProductMasterNotes> PrdProductMasterNoteses { get; set; }
        public virtual DbSet<PrdProductDtNotes> PrdProductDtNoteses { get; set; }
        public virtual DbSet<PrdCategoryDtNotes> PrdCategoryDtNoteses { get; set; }
        public virtual DbSet<CwWebControl> CwWebControls { get; set; }
        public virtual DbSet<BotCustomerInfo> BotCustomerInfos { get; set; }
        public virtual DbSet<BotDomain> BotDomains { get; set; }
        public virtual DbSet<BotQuestion> BotQuestions { get; set; }
        public virtual DbSet<BotAnswer> BotAnswers { get; set; }
        public virtual DbSet<BotQuestionType> BotQuestionTypes { get; set; }
        public virtual DbSet<BotScenario> BotScenarios { get; set; }
        public virtual DbSet<PrjInstalledPlugin> PrjInstalledPlugins { get; set; }
        public gGMSContext(DbContextOptions<gGMSContext> options) : base(options)
        {
        }
        public gGMSContext()
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //    optionsBuilder.UseSqlServer(@"Server=192.168.1.179\sql2012full;Database=gGMS;User Id=gGMS;Password=gGMS@239");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrjInstalledPlugin>().HasKey(p => new { p.PLUGIN_ID, p.PROJECT_ID });
            modelBuilder.Entity<CmsContractFileUpload>(en =>
            {
                en.Property(n => n.FILE_SIZE)
                .HasColumnName("FILE_SIZE")
                .HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<PrdCategoryDt>().HasKey(categorydt => new { categorydt.CATEGORY_ID, categorydt.PRODUCT_ID});

            modelBuilder.Entity<PrdCategoryDtNotes>().HasKey(categorydt => new { categorydt.CATEGORY_ID, categorydt.PRODUCT_ID });

            modelBuilder.Entity<CmAllcode>(entity =>
            {
                entity.ToTable("CM_ALLCODE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cdname)
                    .HasColumnName("CDNAME")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Cdtype)
                    .HasColumnName("CDTYPE")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Cdval)
                    .HasColumnName("CDVAL")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Content)
                    .HasColumnName("CONTENT")
                    .HasMaxLength(500);

                entity.Property(e => e.Lstodr).HasColumnName("LSTODR");
            });

            modelBuilder.Entity<PrjProjectDT>(entity =>
            {
                entity.HasKey(e => new {e.PROJECT_ID, e.EMPLOYEE_ID})
                .HasName("PK_PRJ_PROJECT_DT");
            });

            modelBuilder.Entity<CmsContractDt>(entity =>
            {
                entity.HasKey(e => new { e.ContractId, e.ProductId })
                    .HasName("PK_Contract_Website");

                entity.ToTable("CMS_CONTRACT_DT");

                entity.Property(e => e.ContractId)
                    .HasColumnName("CONTRACT_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("PRODUCT_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ApproveDt)
                    .HasColumnName("APPROVE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthStatus)
                    .HasColumnName("AUTH_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.CheckerId)
                    .HasColumnName("CHECKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.EditDt)
                    .HasColumnName("EDIT_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditorId)
                    .HasColumnName("EDITOR_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.MakerId)
                    .HasColumnName("MAKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Notes)
                    .HasColumnName("NOTES")
                    .HasMaxLength(500);

                entity.Property(e => e.RecordStatus)
                    .HasColumnName("RECORD_STATUS")
                    .HasColumnType("varchar(1)");
            });

            modelBuilder.Entity<CmsContractMaster>(entity =>
            {
                entity.HasKey(e => e.ContractId)
                    .HasName("PK__CMS_CONT__3F5DFF146CEC3216");

                entity.ToTable("CMS_CONTRACT_MASTER");

                entity.Property(e => e.ContractId)
                    .HasColumnName("CONTRACT_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ApproveDt)
                    .HasColumnName("APPROVE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthStatus)
                    .HasColumnName("AUTH_STATUS")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ChargeDt)
                    .HasColumnName("ChargeDT")
                    .HasColumnType("date");

                entity.Property(e => e.CheckerId)
                    .HasColumnName("CHECKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ContractCode)
                    .HasColumnName("CONTRACT_CODE")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CUSTOMER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.DebitBalance).HasColumnType("numeric");

                entity.Property(e => e.DebitMaintainFee).HasColumnType("numeric");

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.DepositAccount).HasColumnType("numeric");

                entity.Property(e => e.DepositAccountBeforLd).HasColumnType("numeric");

                entity.Property(e => e.DepositLiquidation).HasColumnType("numeric");

                entity.Property(e => e.EditDt)
                    .HasColumnName("EDIT_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditorId)
                    .HasColumnName("EDITOR_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.MakerId)
                    .HasColumnName("MAKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Notes)
                    .HasColumnName("NOTES")
                    .HasMaxLength(500);

                entity.Property(e => e.OptimalAmt)
                    .HasColumnName("OptimalAMT")
                    .HasColumnType("numeric");

                entity.Property(e => e.PaidAmt)
                    .HasColumnName("PaidAMT")
                    .HasColumnType("numeric");

                entity.Property(e => e.RecordStatus)
                    .HasColumnName("RECORD_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.SeoAmt)
                    .HasColumnName("SeoAMT")
                    .HasColumnType("numeric");

                entity.Property(e => e.SignContractDt)
                    .HasColumnName("SignContractDT")
                    .HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(256);

                entity.Property(e => e.TypeGoogle).HasColumnType("varchar(15)");

                entity.Property(e => e.Value).HasColumnType("decimal");

                entity.Property(e => e.CONTRACT_TYPE)
                    .HasColumnName("CONTRACT_TYPE")
                    .HasColumnType("varchar(15)");
            });

            modelBuilder.Entity<CmsCustomerMaster>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__CMS_CUST__1CE12D37B86E99BE");

                entity.ToTable("CMS_CUSTOMER_MASTER");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CUSTOMER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Address)
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(500);

                entity.Property(e => e.ApproveDt)
                    .HasColumnName("APPROVE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthStatus)
                    .HasColumnName("AUTH_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.ChargeDt)
                    .HasColumnName("ChargeDT")
                    .HasColumnType("datetime");

                entity.Property(e => e.CheckerId)
                    .HasColumnName("CHECKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CompanyName)
                    .HasColumnName("COMPANY_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerCode)
                    .HasColumnName("CUSTOMER_CODE")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CustomerName)
                    .HasColumnName("CUSTOMER_NAME")
                    .HasMaxLength(70);

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.EditDt)
                    .HasColumnName("EDIT_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditorId)
                    .HasColumnName("EDITOR_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.MakerId)
                    .HasColumnName("MAKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Notes)
                    .HasColumnName("NOTES")
                    .HasMaxLength(500);

                entity.Property(e => e.RecordStatus)
                    .HasColumnName("RECORD_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.SignContractDt)
                    .HasColumnName("SignContractDT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(256);

                entity.Property(e => e.TaxCode)
                    .HasColumnName("TAX_CODE")
                    .HasMaxLength(200);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.Property(e => e.Value).HasColumnType("decimal");
            });

            

            modelBuilder.Entity<PrdPlugin>(entity =>
            {
                entity.HasKey(e => e.PluginId)
                    .HasName("PK__PRD_PLUG__9AB88356F2455F77");

                entity.ToTable("PRD_PLUGIN");

                entity.Property(e => e.PluginId)
                    .HasColumnName("PLUGIN_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ApproveDt)
                    .HasColumnName("APPROVE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthStatus)
                    .HasColumnName("AUTH_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.CheckerId)
                    .HasColumnName("CHECKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.EditDt)
                    .HasColumnName("EDIT_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditorId)
                    .HasColumnName("EDITOR_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.MakerId)
                    .HasColumnName("MAKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Notes)
                    .HasColumnName("NOTES")
                    .HasMaxLength(500);

                entity.Property(e => e.PluginCode)
                    .HasColumnName("PLUGIN_CODE")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.PluginDescription)
                    .HasColumnName("PLUGIN_DESCRIPTION")
                    .HasMaxLength(1000);

                entity.Property(e => e.PluginLocation)
                    .HasColumnName("PLUGIN_LOCATION")
                    .HasMaxLength(1000);

                entity.Property(e => e.PluginName)
                    .HasColumnName("PLUGIN_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.RecordStatus)
                    .HasColumnName("RECORD_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Price)
                .HasColumnName("PRICE").
                HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PriceVat).
                HasColumnName("PRICE_VAT").
                HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Vat).
                HasColumnName("VAT").
                HasColumnType("decimal(5, 2)");

                entity.Property(e => e.DiscountAmt).
                HasColumnName("DISCOUNT_AMT").
                HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PrdProductDt>(entity =>
            {
                entity.HasKey(e => e.ProductDtId)
                    .HasName("PK__PRD_PROD__A18FC336CC24646B_Notes");

                entity.ToTable("PRD_PRODUCT_DT");

                entity.Property(e => e.ProductDtId).HasColumnName("PRODUCT_DT_ID");

                entity.Property(e => e.ApproveDt)
                    .HasColumnName("APPROVE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthStatus)
                    .HasColumnName("AUTH_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.CheckerId)
                    .HasColumnName("CHECKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ComponentId)
                    .HasColumnName("COMPONENT_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditDt)
                    .HasColumnName("EDIT_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditorId)
                    .HasColumnName("EDITOR_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.MakerId)
                    .HasColumnName("MAKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Notes)
                    .HasColumnName("NOTES")
                    .HasMaxLength(500);

                entity.Property(e => e.ProductId)
                    .HasColumnName("PRODUCT_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.RecordStatus)
                    .HasColumnName("RECORD_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasColumnType("varchar(15)");
            });

            modelBuilder.Entity<PrdProductDtNotes>(entity =>
            {
                entity.HasKey(e => e.ProductDtId)
                    .HasName("PK__PRD_PROD__A18FC336CC24646B");

                entity.ToTable("PRD_PRODUCT_DT_NOTES");

                entity.Property(e => e.ProductDtId).HasColumnName("PRODUCT_DT_ID");

                entity.Property(e => e.ApproveDt)
                    .HasColumnName("APPROVE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthStatus)
                    .HasColumnName("AUTH_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.CheckerId)
                    .HasColumnName("CHECKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ComponentId)
                    .HasColumnName("COMPONENT_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditDt)
                    .HasColumnName("EDIT_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditorId)
                    .HasColumnName("EDITOR_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.MakerId)
                    .HasColumnName("MAKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Notes)
                    .HasColumnName("NOTES")
                    .HasMaxLength(500);

                entity.Property(e => e.ProductId)
                    .HasColumnName("PRODUCT_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.RecordStatus)
                    .HasColumnName("RECORD_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasColumnType("varchar(15)");
            });


            modelBuilder.Entity<PrdProductMaster>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_Websites");

                entity.ToTable("PRD_PRODUCT_MASTER");

                entity.Property(e => e.ProductId)
                    .HasColumnName("PRODUCT_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ApproveDt)
                    .HasColumnName("APPROVE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthStatus)
                    .HasColumnName("AUTH_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.CheckerId)
                    .HasColumnName("CHECKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditDt)
                    .HasColumnName("EDIT_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditorId)
                    .HasColumnName("EDITOR_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.MakerId)
                    .HasColumnName("MAKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Notes)
                    .HasColumnName("NOTES")
                    .HasMaxLength(500);

                entity.Property(e => e.ProductCode)
                    .HasColumnName("PRODUCT_CODE")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ProductLocation)
                    .HasColumnName("PRODUCT_LOCATION")
                    .HasMaxLength(1000);

                entity.Property(e => e.ProductName)
                    .HasColumnName("PRODUCT_NAME")
                    .HasMaxLength(500);

                entity.Property(e => e.ProductType)
                    .HasColumnName("PRODUCT_TYPE")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.RecordStatus)
                    .HasColumnName("RECORD_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Price)
                    .HasColumnName("PRICE").
                    HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PriceVat).
                    HasColumnName("PRICE_VAT").
                    HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Vat).
                    HasColumnName("VAT").
                    HasColumnType("decimal(5, 2)");

                entity.Property(e => e.DiscountAmt).
                    HasColumnName("DISCOUNT_AMT").
                    HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Scripts)
                    .HasColumnName("SCRIPTS")
                    .HasColumnType("nvarchar(1024)");
            });

            modelBuilder.Entity<PrdProductMasterNotes>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_Websites_Notes");

                entity.ToTable("PRD_PRODUCT_MASTER_NOTES");

                entity.Property(e => e.ProductId)
                    .HasColumnName("PRODUCT_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ApproveDt)
                    .HasColumnName("APPROVE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthStatus)
                    .HasColumnName("AUTH_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.CheckerId)
                    .HasColumnName("CHECKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditDt)
                    .HasColumnName("EDIT_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditorId)
                    .HasColumnName("EDITOR_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.MakerId)
                    .HasColumnName("MAKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Notes)
                    .HasColumnName("NOTES")
                    .HasMaxLength(500);

                entity.Property(e => e.ProductCode)
                    .HasColumnName("PRODUCT_CODE")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ProductLocation)
                    .HasColumnName("PRODUCT_LOCATION")
                    .HasMaxLength(1000);

                entity.Property(e => e.ProductName)
                    .HasColumnName("PRODUCT_NAME")
                    .HasMaxLength(500);

                entity.Property(e => e.ProductType)
                    .HasColumnName("PRODUCT_TYPE")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.RecordStatus)
                    .HasColumnName("RECORD_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Price)
                    .HasColumnName("PRICE").
                    HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PriceVat).
                    HasColumnName("PRICE_VAT").
                    HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Vat).
                    HasColumnName("VAT").
                    HasColumnType("decimal(5, 2)");

                entity.Property(e => e.DiscountAmt).
                    HasColumnName("DISCOUNT_AMT").
                    HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Scripts)
                    .HasColumnName("SCRIPTS")
                    .HasColumnType("nvarchar(1024)");
            });

            modelBuilder.Entity<PrdSource>(entity =>
            {
                entity.HasKey(e => e.SourceId)
                    .HasName("PK__PRD_SOUR__2630BE820F5D8CAA");

                entity.ToTable("PRD_SOURCE");

                entity.Property(e => e.SourceId)
                    .HasColumnName("SOURCE_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ApproveDt)
                    .HasColumnName("APPROVE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthStatus)
                    .HasColumnName("AUTH_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.CheckerId)
                    .HasColumnName("CHECKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.EditDt)
                    .HasColumnName("EDIT_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditorId)
                    .HasColumnName("EDITOR_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.MakerId)
                    .HasColumnName("MAKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Notes)
                    .HasColumnName("NOTES")
                    .HasMaxLength(500);

                entity.Property(e => e.RecordStatus)
                    .HasColumnName("RECORD_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.SourceCode)
                    .HasColumnName("SOURCE_CODE")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.SourceLocation)
                    .HasColumnName("SOURCE_LOCATION")
                    .HasMaxLength(1000);

                entity.Property(e => e.SourceName)
                    .HasColumnName("SOURCE_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.Price)
                .HasColumnName("PRICE").
                HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PriceVat).
                HasColumnName("PRICE_VAT").
                HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Vat).
                HasColumnName("VAT").
                HasColumnType("decimal(5, 2)");

                entity.Property(e => e.DiscountAmt).
                HasColumnName("DISCOUNT_AMT").
                HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PrdTemplate>(entity =>
            {
                entity.HasKey(e => e.TemplateId)
                    .HasName("PK__PRD_TEMP__BACD412F04D30AB1");

                entity.ToTable("PRD_TEMPLATE");

                entity.Property(e => e.TemplateId)
                    .HasColumnName("TEMPLATE_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ApproveDt)
                    .HasColumnName("APPROVE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthStatus)
                    .HasColumnName("AUTH_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.CheckerId)
                    .HasColumnName("CHECKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.EditDt)
                    .HasColumnName("EDIT_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.EditorId)
                    .HasColumnName("EDITOR_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.MakerId)
                    .HasColumnName("MAKER_ID")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Notes)
                    .HasColumnName("NOTES")
                    .HasMaxLength(500);

                entity.Property(e => e.RecordStatus)
                    .HasColumnName("RECORD_STATUS")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.TemplateCode)
                    .HasColumnName("TEMPLATE_CODE")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.TemplateLocation)
                    .HasColumnName("TEMPLATE_LOCATION")
                    .HasMaxLength(1000);

                entity.Property(e => e.TemplateName)
                    .HasColumnName("TEMPLATE_NAME")
                    .HasMaxLength(256);
                entity.Property(e => e.Price)
                .HasColumnName("PRICE").
                HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PriceVat).
                HasColumnName("PRICE_VAT").
                HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Vat).
                HasColumnName("VAT").
                HasColumnType("decimal(5, 2)");

                entity.Property(e => e.DiscountAmt).
                HasColumnName("DISCOUNT_AMT").
                HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<SysCodemasters>(entity =>
            {
                entity.HasKey(e => e.Prefix)
                    .HasName("PK_SYS_CODEMASTERS");

                entity.ToTable("SYS_CODEMASTERS");

                entity.Property(e => e.Prefix).HasMaxLength(10);

                entity.Property(e => e.Active).HasColumnType("char(1)");

                entity.Property(e => e.CurValue).HasColumnType("decimal");

                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<SysError>(entity =>
            {
                entity.HasKey(e => e.ErrorCode)
                    .HasName("PK_SYS_ERROR");

                entity.ToTable("SYS_ERROR");

                entity.Property(e => e.ErrorCode).HasColumnType("varchar(20)");

                entity.Property(e => e.ErrorDesc).HasMaxLength(1000);

                entity.Property(e => e.Form).HasMaxLength(100);
            });

            modelBuilder.Entity<SysParameters>(entity =>
            {
                entity.HasKey(e => e.ParaKey)
                    .HasName("PK_SYS_PARAMETERS");

                entity.ToTable("SYS_PARAMETERS");

                entity.Property(e => e.ParaKey).HasColumnType("varchar(100)");

                entity.Property(e => e.ApproveDt)
                    .HasColumnName("APPROVE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthStatus)
                    .HasColumnName("AUTH_STATUS")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CheckerId)
                    .HasColumnName("CHECKER_ID")
                    .HasColumnType("varchar(12)");

                entity.Property(e => e.CreateDt)
                    .HasColumnName("CREATE_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataType).HasColumnType("varchar(50)");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MakerId)
                    .HasColumnName("MAKER_ID")
                    .HasColumnType("varchar(12)");

                entity.Property(e => e.ParaValue).HasMaxLength(500);

                entity.Property(e => e.RecordStatus)
                    .HasColumnName("RECORD_STATUS")
                    .HasColumnType("varchar(1)");
            });

            modelBuilder.Entity<SysPrefix>(entity =>
            {
                entity.ToTable("SYS_PREFIX");

                entity.HasIndex(e => e.Prefix)
                    .HasName("IX_SYS_PREFIX")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasColumnType("varchar(10)");
            });
        }
    }
}