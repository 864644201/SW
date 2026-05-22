program Project1;

uses
  Forms,
  ShapeLocation in 'ShapeLocation.pas' {Form1},
  DataofShapeLocation in 'DataofShapeLocation.pas' {Data1: TDataModule};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TData1, Data1);
  Application.CreateForm(Tfrmgccx, frmgccx);
  Application.Run;
end.
