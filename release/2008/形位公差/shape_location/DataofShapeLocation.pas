unit DataofShapeLocation;

interface

uses
  SysUtils, Classes, DB, ADODB;

type
  TData1 = class(TDataModule)
    ADOTable1: TADOTable;
    ADOTable2: TADOTable;
    ADOTable3: TADOTable;
    ADOTable4: TADOTable;
    ADOConnection1: TADOConnection;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Data1: TData1;

implementation

{$R *.dfm}

end.
