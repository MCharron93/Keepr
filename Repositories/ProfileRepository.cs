using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class ProfileRepository
  {
    private readonly IDbConnection _db;
    public ProfileRepository(IDbConnection db)
    {
      _db = db;
    }

    public Profile GetByEmail(string email)
    {
      string sql = "SELECT * FROM kprofile WHERE email = @Email";
      return _db.QueryFirstOrDefault<Profile>(sql, new { email });
    }

    public Profile Create(Profile userInfo)
    {
      string sql = @"
      INSERT INTO kprofile 
      (name, picture, email, id)
      VALUES 
      (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, userInfo);
      return userInfo;
    }

    internal void updateKeepCount(Profile userInfo)
    {
      string sql = @"
      UPDATE kprofile
      SET 
      keeps = @Keeps
      WHERE id = @Id";
      _db.Execute(sql, userInfo);
    }


    internal void updateVaultCount(Profile userInfo)
    {
      string sql = @"
      UPDATE kprofile
      SET 
      vaults = @Vaults
      WHERE id = @Id";
      _db.Execute(sql, userInfo);
    }

  }
}