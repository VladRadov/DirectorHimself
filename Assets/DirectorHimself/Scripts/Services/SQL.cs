using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Connection
{
    private MySqlConnection _connect;

    private static Connection _instance;

    private StringSqlConnection _stringSqlConnection;

    private Connection()
    {
        _stringSqlConnection = new StringSqlConnection();
        _stringSqlConnection.Server = "188.120.236.212";
        _stringSqlConnection.Database = "Director_Himself";
        _stringSqlConnection.UserID = "Director_groot";
        _stringSqlConnection.Password = "1q2w3e4rQ";
        _stringSqlConnection.Pooling = true;

        Create();
    }

    public MySqlConnection Connect => _connect;

    public static Connection Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Connection();

            return _instance;
        }
    }

    private void Create()
    {
        _connect = new MySqlConnection();
        _connect.ConnectionString = 
            $"Server = {_stringSqlConnection.Server}; " +
            $"Database = {_stringSqlConnection.Database}; " +
            $"User ID = {_stringSqlConnection.UserID}; " +
            $"Password = {_stringSqlConnection.Password}; " +
            $"Pooling = {_stringSqlConnection.Pooling.ToString()}";
    }
}

class BaseSqlQuery
{
    protected MySqlCommand _command;

    protected DataTable _tableResult;

    public virtual void Execute()
    {
        var connect = Connection.Instance.Connect;
        _command = Connection.Instance.Connect.CreateCommand();

        if (connect.State != ConnectionState.Open)
            connect.Open();

        CreateCommand();
        CreateQuery();
    }

    protected virtual void CreateCommand()
    {
        _command.CommandType = CommandType.StoredProcedure;
    }

    protected virtual void CreateQuery()
    {
        using (MySqlDataReader reader = _command.ExecuteReader())
        {
            _tableResult = new DataTable();

            for (int i = 0; i < reader.FieldCount; i++)
                _tableResult.Columns.Add(reader.GetName(i));

            int countRows = 0;
            while (reader.Read())
            {
                object[] subitems = new object[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                    subitems[i] = reader.GetValue(i);

                var row = _tableResult.NewRow();
                row.ItemArray = subitems;
                _tableResult.Rows.Add(row);

                ++countRows;
            }
        }
    }

    public int CounItems() => _tableResult.Rows.Count;

    public int CounColumns() => _tableResult.Columns.Count;

    public object ParsingTableResult(int indexRow, int indexColumn) => _tableResult.Rows[indexRow].ItemArray[indexColumn];
}

class GetPlayer : BaseSqlQuery
{
    private string _nickname;

    private string _email;

    public GetPlayer(string nickname, string email)
    {
        _nickname = nickname;
        _email = email;
    }

    public override void Execute() => base.Execute();

    protected override void CreateCommand()
    {
        _command.CommandType = CommandType.StoredProcedure;
        _command.CommandText = "GetPlayer";
        _command.Parameters.AddWithValue("InNickname", _nickname);
        _command.Parameters.AddWithValue("InEmail", _email);
    }

    protected override void CreateQuery() => base.CreateQuery();
}

class GetCartoons : BaseSqlQuery
{
    private string _nickname;

    private string _email;

    public GetCartoons(string nickname, string email)
    {
        _nickname = nickname;
        _email = email;
    }

    public override void Execute() => base.Execute();

    protected override void CreateCommand()
    {
        _command.CommandType = CommandType.StoredProcedure;
        _command.CommandText = "GetCartoons";
        _command.Parameters.AddWithValue("NickPlayer", _nickname);
        _command.Parameters.AddWithValue("EmailPlayer", _email);
    }

    protected override void CreateQuery() => base.CreateQuery();
}

class AddCartoon : BaseSqlQuery
{
    private string _nameCartoon;

    private int _idPlayer;

    public AddCartoon(int idPlayer, string nameCartoon)
    {
        _nameCartoon = nameCartoon;
        _idPlayer = idPlayer;
    }

    public override void Execute() => base.Execute();

    protected override void CreateCommand()
    {
        _command.CommandType = CommandType.StoredProcedure;
        _command.CommandText = "AddCartoon";
        _command.Parameters.AddWithValue("InName", _nameCartoon);
        _command.Parameters.AddWithValue("InIdPlayer", _idPlayer);
    }

    protected override void CreateQuery() => base.CreateQuery();
}

class AddCartoonObject : BaseSqlQuery
{
    private IObjectCartoon _cartoonObject;

    private int _idPlayer;

    private int _idCartoon;

    private string _nameObjectCartoon;

    public AddCartoonObject(int idPlayer, int idCartoon, string nameObjectCartoon, IObjectCartoon cartoonObject)
    {
        _idPlayer = idPlayer;
        _idCartoon = idCartoon;
        _nameObjectCartoon = nameObjectCartoon;
        _cartoonObject = cartoonObject;
    }

    public override void Execute() => base.Execute();

    protected override void CreateCommand()
    {
        _command.CommandType = CommandType.StoredProcedure;
        _command.CommandText = "AddCartoonObject";
        _command.Parameters.AddWithValue("InIdPlayer", _idPlayer);
        _command.Parameters.AddWithValue("InIdCartoon", _idCartoon);
        _command.Parameters.AddWithValue("InNameObjectCartoon", _nameObjectCartoon);
        _command.Parameters.AddWithValue("InPositionX", _cartoonObject.PositionStartX);
        _command.Parameters.AddWithValue("InPositionY", _cartoonObject.PositionStartY);
        _command.Parameters.AddWithValue("InSizeX", _cartoonObject.ScaleX);
        _command.Parameters.AddWithValue("InSizeY", _cartoonObject.ScaleY);
    }

    protected override void CreateQuery() => base.CreateQuery();
}

class GetObjectsCartoon : BaseSqlQuery
{
    private int _idPlayer;

    private string _nameCartoon;

    public GetObjectsCartoon(int idPlayer, string nameCartoon)
    {
        _idPlayer = idPlayer;
        _nameCartoon = nameCartoon;
    }

    public override void Execute() => base.Execute();

    protected override void CreateCommand()
    {
        _command.CommandType = CommandType.StoredProcedure;
        _command.CommandText = "GetObjectsCartoon";
        _command.Parameters.AddWithValue("InIdPLayer", _idPlayer);
        _command.Parameters.AddWithValue("InNameObjectCartoon", _nameCartoon);
    }

    protected override void CreateQuery() => base.CreateQuery();
}

class ChangeStartPositionObjectCartoon : BaseSqlQuery
{
    private int _idObjectCartoon;

    private float _positionX;

    private float _positionY;

    public ChangeStartPositionObjectCartoon(int idObjectCartoon, float positionX, float positionY)
    {
        _idObjectCartoon = idObjectCartoon;
        _positionX = positionX;
        _positionY = positionY;
    }

    public override void Execute() => base.Execute();

    protected override void CreateCommand()
    {
        _command.CommandType = CommandType.StoredProcedure;
        _command.CommandText = "ChangeStartPositionObjectCartoon";
        _command.Parameters.AddWithValue("InIdObjectCartoon", _idObjectCartoon);
        _command.Parameters.AddWithValue("InPositionX", _positionX);
        _command.Parameters.AddWithValue("InPositionY", _positionY);
    }

    protected override void CreateQuery() => base.CreateQuery();
}