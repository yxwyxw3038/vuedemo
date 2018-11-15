﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestConsoleDemo.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AchieveDBEntities : DbContext
    {
        public AchieveDBEntities()
            : base("name=AchieveDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<tb_Test> tb_Test { get; set; }
        public virtual DbSet<tb_Test1> tb_Test1 { get; set; }
        public virtual DbSet<tbButton> tbButton { get; set; }
        public virtual DbSet<tbDataType> tbDataType { get; set; }
        public virtual DbSet<tbDepartment> tbDepartment { get; set; }
        public virtual DbSet<tbFields> tbFields { get; set; }
        public virtual DbSet<tbHtmlType> tbHtmlType { get; set; }
        public virtual DbSet<tbIcons> tbIcons { get; set; }
        public virtual DbSet<tbMenu> tbMenu { get; set; }
        public virtual DbSet<tbMenuButton> tbMenuButton { get; set; }
        public virtual DbSet<tbNews> tbNews { get; set; }
        public virtual DbSet<tbNewsType> tbNewsType { get; set; }
        public virtual DbSet<tbRequestion> tbRequestion { get; set; }
        public virtual DbSet<tbRequestionType> tbRequestionType { get; set; }
        public virtual DbSet<tbRole> tbRole { get; set; }
        public virtual DbSet<tbRoleMenu> tbRoleMenu { get; set; }
        public virtual DbSet<tbRoleMenuButton> tbRoleMenuButton { get; set; }
        public virtual DbSet<tbTable> tbTable { get; set; }
        public virtual DbSet<tbUser> tbUser { get; set; }
        public virtual DbSet<tbUserDepartment> tbUserDepartment { get; set; }
        public virtual DbSet<tbUserRole> tbUserRole { get; set; }
        public virtual DbSet<vw_Fields> vw_Fields { get; set; }
        public virtual DbSet<vw_News> vw_News { get; set; }
        public virtual DbSet<vw_requestion> vw_requestion { get; set; }
    
        [DbFunction("AchieveDBEntities", "func_SplitStrToTable")]
        public virtual IQueryable<func_SplitStrToTable_Result> func_SplitStrToTable(string str)
        {
            var strParameter = str != null ?
                new ObjectParameter("str", str) :
                new ObjectParameter("str", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<func_SplitStrToTable_Result>("[AchieveDBEntities].[func_SplitStrToTable](@str)", strParameter);
        }
    
        public virtual int sp_CreateTable(string tabName)
        {
            var tabNameParameter = tabName != null ?
                new ObjectParameter("TabName", tabName) :
                new ObjectParameter("TabName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_CreateTable", tabNameParameter);
        }
    
        public virtual ObjectResult<sp_GetAuthorityByUserId_Result> sp_GetAuthorityByUserId(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAuthorityByUserId_Result>("sp_GetAuthorityByUserId", userIdParameter);
        }
    
        public virtual int sp_MvcPager(Nullable<int> pageSize, Nullable<int> currentCount, string tableName, string where, string order, ObjectParameter totalCount)
        {
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var currentCountParameter = currentCount.HasValue ?
                new ObjectParameter("CurrentCount", currentCount) :
                new ObjectParameter("CurrentCount", typeof(int));
    
            var tableNameParameter = tableName != null ?
                new ObjectParameter("TableName", tableName) :
                new ObjectParameter("TableName", typeof(string));
    
            var whereParameter = where != null ?
                new ObjectParameter("Where", where) :
                new ObjectParameter("Where", typeof(string));
    
            var orderParameter = order != null ?
                new ObjectParameter("Order", order) :
                new ObjectParameter("Order", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_MvcPager", pageSizeParameter, currentCountParameter, tableNameParameter, whereParameter, orderParameter, totalCount);
        }
    
        public virtual int sp_Pager(string tableName, string columns, string order, Nullable<int> pageSize, Nullable<int> pageIndex, string where, ObjectParameter totalCount)
        {
            var tableNameParameter = tableName != null ?
                new ObjectParameter("tableName", tableName) :
                new ObjectParameter("tableName", typeof(string));
    
            var columnsParameter = columns != null ?
                new ObjectParameter("columns", columns) :
                new ObjectParameter("columns", typeof(string));
    
            var orderParameter = order != null ?
                new ObjectParameter("order", order) :
                new ObjectParameter("order", typeof(string));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("pageSize", pageSize) :
                new ObjectParameter("pageSize", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("pageIndex", pageIndex) :
                new ObjectParameter("pageIndex", typeof(int));
    
            var whereParameter = where != null ?
                new ObjectParameter("where", where) :
                new ObjectParameter("where", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Pager", tableNameParameter, columnsParameter, orderParameter, pageSizeParameter, pageIndexParameter, whereParameter, totalCount);
        }
    
        public virtual int sp_SetRoleAuthorize(Nullable<int> roleId, string menuButtonIds)
        {
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("RoleId", roleId) :
                new ObjectParameter("RoleId", typeof(int));
    
            var menuButtonIdsParameter = menuButtonIds != null ?
                new ObjectParameter("MenuButtonIds", menuButtonIds) :
                new ObjectParameter("MenuButtonIds", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_SetRoleAuthorize", roleIdParameter, menuButtonIdsParameter);
        }
    }
}
