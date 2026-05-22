object Data1: TData1
  OldCreateOrder = False
  Left = 415
  Top = 222
  Height = 150
  Width = 423
  object ADOTable1: TADOTable
    Connection = ADOConnection1
    CursorType = ctStatic
    TableName = #30452#32447#24179#38754#24230
    Left = 40
    Top = 8
  end
  object ADOTable2: TADOTable
    Connection = ADOConnection1
    CursorType = ctStatic
    TableName = #22278#24230#22278#26609#24230
    Left = 112
    Top = 8
  end
  object ADOTable3: TADOTable
    Connection = ADOConnection1
    CursorType = ctStatic
    TableName = #24179#34892#22402#30452#20542#26012#24230
    Left = 192
    Top = 8
  end
  object ADOTable4: TADOTable
    Connection = ADOConnection1
    CursorType = ctStatic
    TableName = #21516#36724#23545#31216#36339#21160
    Left = 272
    Top = 8
  end
  object ADOConnection1: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Password="";User ID=Admin;Data ' +
      'Source=D:\jxsjsc_new\shape_location\DATAS\cooper.mdb;Mode=Share ' +
      'Deny None;Extended Properties="";Persist Security Info=True;Jet ' +
      'OLEDB:System database="";Jet OLEDB:Registry Path="";Jet OLEDB:Da' +
      'tabase Password="";Jet OLEDB:Engine Type=5;Jet OLEDB:Database Lo' +
      'cking Mode=1;Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Globa' +
      'l Bulk Transactions=1;Jet OLEDB:New Database Password="";Jet OLE' +
      'DB:Create System Database=False;Jet OLEDB:Encrypt Database=False' +
      ';Jet OLEDB:Don'#39't Copy Locale on Compact=False;Jet OLEDB:Compact ' +
      'Without Replica Repair=False;Jet OLEDB:SFP=False'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 32
    Top = 72
  end
end
