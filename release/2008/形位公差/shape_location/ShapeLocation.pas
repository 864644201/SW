unit ShapeLocation;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, ImgList,DB, ComCtrls, Buttons, Menus,ShellAPI;

type
  Tfrmgccx = class(TForm)
    ImageList1: TImageList;
    Label1: TLabel;
    Edit1: TEdit;
    Label2: TLabel;
    Label3: TLabel;
    ComboBox1: TComboBox;
    Label4: TLabel;
    RadioGroup1: TRadioGroup;
    RadioGroup2: TRadioGroup;
    RadioButton3: TRadioButton;
    RadioButton4: TRadioButton;
    RadioButton5: TRadioButton;
    RadioButton6: TRadioButton;
    RadioButton7: TRadioButton;
    RadioButton8: TRadioButton;
    RadioButton9: TRadioButton;
    RadioButton10: TRadioButton;
    RadioButton11: TRadioButton;
    RadioButton12: TRadioButton;
    RadioButton13: TRadioButton;
    CheckBox1: TCheckBox;
    CheckBox2: TCheckBox;
    LB: TLabel;
    BitBtn1: TBitBtn;
    Label7: TLabel;
    Image1: TImage;
    MainMenu1: TMainMenu;
    N1: TMenuItem;
    N4: TMenuItem;
    N2: TMenuItem;
    N3: TMenuItem;
    N5: TMenuItem;
    C1: TMenuItem;
    procedure RadioButton3Click(Sender: TObject);
    procedure RadioButton4Click(Sender: TObject);
    procedure RadioButton5Click(Sender: TObject);
    procedure RadioButton6Click(Sender: TObject);
    procedure RadioButton7Click(Sender: TObject);
    procedure RadioButton8Click(Sender: TObject);
    procedure RadioButton9Click(Sender: TObject);
    procedure RadioButton10Click(Sender: TObject);
    procedure RadioButton11Click(Sender: TObject);
    procedure RadioButton12Click(Sender: TObject);
    procedure RadioButton13Click(Sender: TObject);
    procedure CheckBox1Click(Sender: TObject);
    procedure CheckBox2Click(Sender: TObject);
    procedure Edit1Change(Sender: TObject);
    procedure ComboBox1Change(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure BitBtn1Click(Sender: TObject);
    procedure C1Click(Sender: TObject);
   
  private
    { Private declarations }
  public
    { Public declarations }
    //MainPara : Integer;
    GCDJNum : Single;
    GCDJ : String;
    GCValue : Single;
    BasicSize :Single;

  end;

var
  frmgccx: Tfrmgccx;
  mpath : string;
implementation

uses DataofShapeLocation;

{$R *.dfm}

procedure Tfrmgccx.RadioButton3Click(Sender: TObject);
var
BasicSize1 :Single;
begin
  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    Edit1.Text :=IntToStr(1);
    Edit1.SelectAll;
  end
  else if (Edit1.Text ='') or (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >10000) then
    BasicSize :=0
  else
    BasicSize :=StrToFloat(Edit1.Text);

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if RadioButton3.Checked = true then
  begin
    Label7.Caption :='主参数L图例';
    Image1.Picture.LoadFromFile(mpath+'zxd.bmp');
  end;

  //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
  Data1.ADOTable1.First;
  while not Data1.ADOTable1.Eof do
  begin
    if  BasicSize <=Data1.ADOTable1.FieldByName('主参数').AsFloat then
    begin
      BasicSize1 := Data1.ADOTable1.FieldByName('主参数').AsFloat;
      Break;
    end;
    Data1.ADOTable1.Next;
  end;

  Data1.ADOTable1.First;
  if Data1.ADOTable1.Locate('主参数',BasicSize1,[loPartialKey]) then
  begin
    GCValue := Data1.ADOTable1.FieldByName(GCDJ).AsFloat;
  end;
  GCValue :=GCValue*1000;
 
  LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
end;

procedure Tfrmgccx.RadioButton4Click(Sender: TObject);
var
BasicSize1 :Single;
begin
  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    Edit1.Text :=FloatToStr(1);
    Edit1.SelectAll;
  end
  else if (Edit1.Text ='') or (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >10000) then
    BasicSize :=0
  else
    BasicSize :=StrToFloat(Edit1.Text);

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if RadioButton4.Checked = true then
  begin
    Label7.Caption :='主参数L图例';
    Image1.Picture.LoadFromFile(mpath+'pmd.bmp');
  end;

  //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
  Data1.ADOTable1.First;
  while not Data1.ADOTable1.Eof do
  begin
    if  BasicSize <=Data1.ADOTable1.FieldByName('主参数').AsFloat then
    begin
      BasicSize1 := Data1.ADOTable1.FieldByName('主参数').AsFloat;
      Break;
    end;
    Data1.ADOTable1.Next;
  end;

  Data1.ADOTable1.First;
  if Data1.ADOTable1.Locate('主参数',BasicSize1,[loPartialKey]) then
  begin
    GCValue := Data1.ADOTable1.FieldByName(GCDJ).AsFloat;
  end;
  GCValue :=GCValue*1000;
  
  LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
end;

procedure Tfrmgccx.RadioButton5Click(Sender: TObject);
var
BasicSize2 :Single;
begin
  if (StrToFloat(Edit1.Text)>500) then
    begin
      MessageBox (
      HWND (0),
      LPCTSTR ('请输入大于0小于等于500的整数'),
      LPCTSTR ('提示'),
      UINT (MB_OK)
      );
      Edit1.Text :=FloatToStr(StrToFloat(Edit1.Text)/(10.0));
      Edit1.SetFocus;
      Edit1.SelectAll;
    end;

  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    Edit1.Text :=FloatToStr(1);
    Edit1.SelectAll;
  end
  else if (Edit1.Text ='') or (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >500) then
    BasicSize :=0
  else
    BasicSize :=StrToFloat(Edit1.Text);

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if RadioButton5.Checked = true then
  begin
    Label7.Caption :='主参数L图例';
    Image1.Picture.LoadFromFile(mpath+'yd.bmp');
  end;

  //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
  Data1.ADOTable2.First;
  while not Data1.ADOTable2.Eof do
  begin
    if  BasicSize <=Data1.ADOTable2.FieldByName('主参数').AsFloat then
    begin
    BasicSize2 := Data1.ADOTable2.FieldByName('主参数').AsFloat;
    Break;
    end;
    Data1.ADOTable2.Next;
  end;

  Data1.ADOTable2.First;
  if Data1.ADOTable2.Locate('主参数',BasicSize2,[loPartialKey]) then
  begin
    GCValue := Data1.ADOTable2.FieldByName(GCDJ).AsFloat;
  end;
  GCValue :=GCValue*1000;
  
  LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
end;

procedure Tfrmgccx.RadioButton6Click(Sender: TObject);
var
BasicSize2 :Single;
begin
  if (StrToInt(Edit1.Text)>500) then
    begin
      MessageBox (
      HWND (0),
      LPCTSTR ('请输入大于0小于等于500的整数'),
      LPCTSTR ('提示'),
      UINT (MB_OK)
      );
      Edit1.Text :=FloatToStr(StrToFloat(Edit1.Text)/(10.0));
      Edit1.SetFocus;
      Edit1.SelectAll;
    end;

  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    Edit1.Text :=FloatToStr(1);
    Edit1.SelectAll;
  end
  else if (Edit1.Text ='') or (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >500) then
    BasicSize :=0
  else
    BasicSize :=StrToFloat(Edit1.Text);

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if RadioButton6.Checked = true then
  begin
    Label7.Caption :='主参数L图例';
    Image1.Picture.LoadFromFile(mpath+'yzd.bmp');
  end;

  //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
  Data1.ADOTable2.First;
  while not Data1.ADOTable2.Eof do
  begin
    if  BasicSize <=Data1.ADOTable2.FieldByName('主参数').AsFloat then
    begin
    BasicSize2 := Data1.ADOTable2.FieldByName('主参数').AsFloat;
    Break;
    end;
    Data1.ADOTable2.Next;
  end;

  Data1.ADOTable2.First;
  if Data1.ADOTable2.Locate('主参数',BasicSize2,[loPartialKey]) then
  begin
    GCValue := Data1.ADOTable2.FieldByName(GCDJ).AsFloat;
  end;
  GCValue :=GCValue*1000;
  
  LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
end;

procedure Tfrmgccx.RadioButton7Click(Sender: TObject);
var
BasicSize3 :Single;
begin
  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    Edit1.Text :=FloatToStr(1);
    Edit1.SelectAll;
  end
  else if (Edit1.Text ='') or (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >10000) then
    BasicSize :=0
  else
    BasicSize :=StrToFloat(Edit1.Text);

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if RadioButton7.Checked = true then
  begin
    Label7.Caption :='主参数L,d(D)图例';
    Image1.Picture.LoadFromFile(mpath+'pxd.bmp');
  end;

  //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
  Data1.ADOTable3.First;
  while not Data1.ADOTable3.Eof do
  begin
    if  BasicSize <=Data1.ADOTable3.FieldByName('主参数').AsFloat then
    begin
      BasicSize3 := Data1.ADOTable3.FieldByName('主参数').AsFloat;
      Break;
    end;
    Data1.ADOTable3.Next;
  end;

  Data1.ADOTable3.First;
  if Data1.ADOTable3.Locate('主参数',BasicSize3,[loPartialKey]) then
  begin
    GCValue := Data1.ADOTable3.FieldByName(GCDJ).AsFloat;
  end;
  GCValue :=GCValue*1000;

  LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
end;

procedure Tfrmgccx.RadioButton8Click(Sender: TObject);
var
BasicSize3 :Single;
begin
  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    Edit1.Text :=FloatToStr(1);
    Edit1.SelectAll;
  end
  else if (Edit1.Text ='') or (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >10000) then
    BasicSize :=0
  else
    BasicSize :=StrToFloat(Edit1.Text);

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if RadioButton8.Checked = true then
  begin
    Label7.Caption :='主参数L,d(D)图例';
    Image1.Picture.LoadFromFile(mpath+'czd.bmp');
  end;

  //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
  Data1.ADOTable3.First;
  while not Data1.ADOTable3.Eof do
  begin
    if  BasicSize <=Data1.ADOTable3.FieldByName('主参数').AsFloat then
    begin
      BasicSize3 := Data1.ADOTable3.FieldByName('主参数').AsFloat;
      Break;
    end;
    Data1.ADOTable3.Next;
  end;

  Data1.ADOTable3.First;
  if Data1.ADOTable3.Locate('主参数',BasicSize3,[loPartialKey]) then
  begin
    GCValue := Data1.ADOTable3.FieldByName(GCDJ).AsFloat;
  end;
  GCValue :=GCValue*1000;
  
  LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
end;

procedure Tfrmgccx.RadioButton9Click(Sender: TObject);
var
BasicSize3 :Single;
begin
  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    Edit1.Text :=FloatToStr(1);
    Edit1.SelectAll;
  end
  else if (Edit1.Text ='') or (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >10000) then
    BasicSize :=0
  else
    BasicSize :=StrToFloat(Edit1.Text);

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if RadioButton9.Checked = true then
  begin
    Label7.Caption :='主参数L,d(D)图例';
    Image1.Picture.LoadFromFile(mpath+'qxd.bmp');
  end;

  //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
  Data1.ADOTable3.First;
  while not Data1.ADOTable3.Eof do
  begin
    if  BasicSize <=Data1.ADOTable3.FieldByName('主参数').AsFloat then
    begin
      BasicSize3 := Data1.ADOTable3.FieldByName('主参数').AsFloat;
      Break;
    end;
    Data1.ADOTable3.Next;
  end;

  Data1.ADOTable3.First;
  if Data1.ADOTable3.Locate('主参数',BasicSize3,[loPartialKey]) then
  begin
    GCValue := Data1.ADOTable3.FieldByName(GCDJ).AsFloat;
  end;
  GCValue :=GCValue*1000;
  
  LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
end;

procedure Tfrmgccx.RadioButton10Click(Sender: TObject);
var
BasicSize4 :Single;
begin
  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    Edit1.Text :=FloatToStr(1);
    Edit1.SelectAll;
  end
  else if (Edit1.Text ='') or (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >10000) then
    BasicSize :=0
  else
    BasicSize :=StrToFloat(Edit1.Text);

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if RadioButton10.Checked = true then
  begin
    Label7.Caption :='主参数d(D)图例';
    Image1.Picture.LoadFromFile(mpath+'tzd.bmp');
  end;

  //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
  Data1.ADOTable4.First;
  while not Data1.ADOTable4.Eof do
  begin
    if  BasicSize <=Data1.ADOTable4.FieldByName('主参数').AsFloat then
    begin
      BasicSize4 := Data1.ADOTable4.FieldByName('主参数').AsFloat;
      Break;
    end;
    Data1.ADOTable4.Next;
  end;

  Data1.ADOTable4.First;
  if Data1.ADOTable4.Locate('主参数',BasicSize4,[loPartialKey]) then
  begin
    GCValue := Data1.ADOTable4.FieldByName(GCDJ).AsFloat;
  end;
  GCValue :=GCValue*1000;
  
  LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
end;

procedure Tfrmgccx.RadioButton11Click(Sender: TObject);
var
BasicSize4 :Single;
begin
  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    Edit1.Text :=FloatToStr(1);
    Edit1.SelectAll;
  end
  else if (Edit1.Text ='') or (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >10000) then
    BasicSize :=0
  else
    BasicSize :=StrToFloat(Edit1.Text);

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if RadioButton11.Checked = true then
  begin
    Label7.Caption :='主参数B,L图例';
    Image1.Picture.LoadFromFile(mpath+'dcd.bmp');
  end;

  //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
  Data1.ADOTable4.First;
  while not Data1.ADOTable4.Eof do
  begin
    if  BasicSize <=Data1.ADOTable4.FieldByName('主参数').AsFloat then
    begin
      BasicSize4 := Data1.ADOTable4.FieldByName('主参数').AsFloat;
      Break;
    end;
    Data1.ADOTable4.Next;
  end;

  Data1.ADOTable4.First;
  if Data1.ADOTable4.Locate('主参数',BasicSize4,[loPartialKey]) then
  begin
    GCValue := Data1.ADOTable4.FieldByName(GCDJ).AsFloat;
  end;
  GCValue :=GCValue*1000;
  
  LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
end;

procedure Tfrmgccx.RadioButton12Click(Sender: TObject);
var
BasicSize4 :Single;
begin
  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    Edit1.Text :=FloatToStr(1);
    Edit1.SelectAll;
  end
  else if (Edit1.Text ='') or (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >10000) then
    BasicSize :=0
  else
    BasicSize :=StrToFloat(Edit1.Text);

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if RadioButton12.Checked = true then
  begin
    Label7.Caption :='主参数d(D)图例';
    Image1.Picture.LoadFromFile(mpath+'ytd.bmp');
  end;

  //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
  Data1.ADOTable4.First;
  while not Data1.ADOTable4.Eof do
  begin
    if  BasicSize <=Data1.ADOTable4.FieldByName('主参数').AsFloat then
    begin
      BasicSize4 := Data1.ADOTable4.FieldByName('主参数').AsFloat;
      Break;
    end;
    Data1.ADOTable4.Next;
  end;

  Data1.ADOTable4.First;
  if Data1.ADOTable4.Locate('主参数',BasicSize4,[loPartialKey]) then
  begin
    GCValue := Data1.ADOTable4.FieldByName(GCDJ).AsFloat;
  end;
  GCValue :=GCValue*1000;

  LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
end;

procedure Tfrmgccx.RadioButton13Click(Sender: TObject);
var
BasicSize4 :Single;
begin
  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
    BasicSize :=0;
    Edit1.Text :=FloatToStr(1);
    Edit1.SelectAll;
  end
  else if (Edit1.Text ='') or (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >10000) then
    BasicSize :=0
  else
    BasicSize :=StrToFloat(Edit1.Text);

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if RadioButton13.Checked = true then
  begin
    Label7.Caption :='主参数d(D)图例';
    Image1.Picture.LoadFromFile(mpath+'qtd.bmp');
  end;

  //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
  Data1.ADOTable4.First;
  while not Data1.ADOTable4.Eof do
  begin
    if  BasicSize <=Data1.ADOTable4.FieldByName('主参数').AsFloat then
    begin
      BasicSize4 := Data1.ADOTable4.FieldByName('主参数').AsFloat;
      Break;
    end;
    Data1.ADOTable4.Next;
  end;

  Data1.ADOTable4.First;
  if Data1.ADOTable4.Locate('主参数',BasicSize4,[loPartialKey]) then
  begin
    GCValue := Data1.ADOTable4.FieldByName(GCDJ).AsFloat;
  end;
  GCValue :=GCValue*1000;
  
  LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
end;

procedure Tfrmgccx.CheckBox1Click(Sender: TObject);
begin
  LB.Caption:='';
  if (CheckBox1.Checked = False) and (CheckBox2.Checked = False) then
  begin
    RadioButton3.Enabled :=False;
    RadioButton4.Enabled :=False;
    RadioButton5.Enabled :=False;
    RadioButton6.Enabled :=False;
    RadioButton7.Enabled :=False;
    RadioButton8.Enabled :=False;
    RadioButton9.Enabled :=False;
    RadioButton10.Enabled :=False;
    RadioButton11.Enabled :=False;
    RadioButton12.Enabled :=False;
    RadioButton13.Enabled :=False;
  end
  else if (CheckBox1.Checked = true) then
  begin
    CheckBox2.Checked :=False;
    RadioButton3.Enabled :=true;
    RadioButton4.Enabled :=true;
    RadioButton5.Enabled :=true;
    RadioButton6.Enabled :=true;
    RadioButton7.Enabled :=true;
    RadioButton8.Enabled :=true;
    RadioButton9.Enabled :=False;
    RadioButton10.Enabled :=False;
    RadioButton11.Enabled :=False;
    RadioButton12.Enabled :=False;
    RadioButton13.Enabled :=False;

    RadioButton3.Checked:=true;
  end;
end;

procedure Tfrmgccx.CheckBox2Click(Sender: TObject);
begin
  LB.Caption:='';
  if (CheckBox1.Checked = False) and (CheckBox2.Checked = False) then
  begin
    RadioButton3.Enabled :=False;
    RadioButton4.Enabled :=False;
    RadioButton5.Enabled :=False;
    RadioButton6.Enabled :=False;
    RadioButton7.Enabled :=False;
    RadioButton8.Enabled :=False;
    RadioButton9.Enabled :=False;
    RadioButton10.Enabled :=False;
    RadioButton11.Enabled :=False;
    RadioButton12.Enabled :=False;
    RadioButton13.Enabled :=False;
  end
  else if (CheckBox2.Checked = true) then
  begin
    CheckBox1.Checked :=False;
    RadioButton3.Enabled :=False;
    RadioButton4.Enabled :=False;
    RadioButton5.Enabled :=False;
    RadioButton6.Enabled :=False;
    RadioButton7.Enabled :=False;
    RadioButton8.Enabled :=False;
    RadioButton9.Enabled :=true;
    RadioButton10.Enabled :=true;
    RadioButton11.Enabled :=true;
    RadioButton12.Enabled :=true;
    RadioButton13.Enabled :=true;

    RadioButton9.Checked:=true;
  end;
end;

procedure Tfrmgccx.Edit1Change(Sender: TObject);
var
BasicSize1 :Single;
BasicSize2 :Single;
BasicSize3 :Single;
BasicSize4 :Single;
begin
  LB.Caption:='';
  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
  BasicSize :=0;
  Edit1.Text :=FloatToStr(1);
  Edit1.SelectAll;
  end

  else if (Edit1.Text ='') then
  begin
    BasicSize :=0;
    Edit1.SelectAll;
    CheckBox1.Checked :=False;
  end

  else if (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >10000) then
  begin
    BasicSize :=0;
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入大于0小于等于10000的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    Edit1.Text :=FloatToStr(StrToFloat(Edit1.Text)/(10.0));
    Edit1.SelectAll;
    CheckBox1.Checked :=False;
  end

  else
  begin
 //   CheckBox1.Checked :=true;
    BasicSize :=StrToFloat(Edit1.Text);
  end;

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;

  if (RadioButton3.Checked = true) or (RadioButton4.Checked = true) then
  begin

    //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
    Data1.ADOTable1.First;
    while not Data1.ADOTable1.Eof do
    begin
      if  BasicSize <=Data1.ADOTable1.FieldByName('主参数').AsFloat then
      begin
        BasicSize1 := Data1.ADOTable1.FieldByName('主参数').AsFloat;
        Break;
      end;
      Data1.ADOTable1.Next;
    end;

    Data1.ADOTable1.First;
    if Data1.ADOTable1.Locate('主参数',BasicSize1,[loPartialKey]) then
    begin
      GCValue := Data1.ADOTable1.FieldByName(GCDJ).AsFloat;
    end;
    GCValue :=GCValue*1000;
    
    LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
  end;

  if (RadioButton5.Checked = true) or (RadioButton6.Checked = true) then
  begin
    if (StrToFloat(Edit1.Text)>500) then
    begin
      MessageBox (
      HWND (0),
      LPCTSTR ('请输入大于0小于等于500的整数'),
      LPCTSTR ('提示'),
      UINT (MB_OK)
      );
      Edit1.Text :=FloatToStr(StrToFloat(Edit1.Text)/(10.0));
      Edit1.SelectAll;
    end;

    //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
    Data1.ADOTable2.First;
    while not Data1.ADOTable2.Eof do
    begin
      if  BasicSize <=Data1.ADOTable2.FieldByName('主参数').AsFloat then
      begin
        BasicSize2 := Data1.ADOTable2.FieldByName('主参数').AsFloat;
        Break;
      end;
      Data1.ADOTable2.Next;
    end;

    Data1.ADOTable2.First;
    if Data1.ADOTable2.Locate('主参数',BasicSize2,[loPartialKey]) then
    begin
      GCValue := Data1.ADOTable2.FieldByName(GCDJ).AsFloat;
    end;
    GCValue :=GCValue*1000;
    
    LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
  end;

  if (RadioButton7.Checked = true) or (RadioButton8.Checked = true) or (RadioButton9.Checked = true) then
  begin

    //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
    Data1.ADOTable3.First;
    while not Data1.ADOTable3.Eof do
    begin
      if  BasicSize <=Data1.ADOTable3.FieldByName('主参数').AsFloat then
      begin
        BasicSize3 := Data1.ADOTable3.FieldByName('主参数').AsFloat;
        Break;
      end;
      Data1.ADOTable3.Next;
    end;

    Data1.ADOTable3.First;
    if Data1.ADOTable3.Locate('主参数',BasicSize3,[loPartialKey]) then
    begin
      GCValue := Data1.ADOTable3.FieldByName(GCDJ).AsFloat;
    end;
    GCValue :=GCValue*1000;
    
    LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
  end;

  if (RadioButton10.Checked = true) or (RadioButton11.Checked = true)
      or (RadioButton12.Checked = true) or (RadioButton13.Checked = true) then
  begin

    //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
    Data1.ADOTable4.First;
    while not Data1.ADOTable4.Eof do
    begin
      if  BasicSize <=Data1.ADOTable4.FieldByName('主参数').AsFloat then
      begin
        BasicSize4 := Data1.ADOTable4.FieldByName('主参数').AsFloat;
        Break;
      end;
      Data1.ADOTable4.Next;
    end;

    Data1.ADOTable4.First;
    if Data1.ADOTable4.Locate('主参数',BasicSize4,[loPartialKey]) then
    begin
      GCValue := Data1.ADOTable4.FieldByName(GCDJ).AsFloat;
    end;
    GCValue :=GCValue*1000;

    LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
  end;
end;

procedure Tfrmgccx.ComboBox1Change(Sender: TObject);
var
BasicSize1 :Single;
BasicSize2 :Single;
BasicSize3 :Single;
BasicSize4 :Single;
begin
  LB.Caption:='';
  if (Edit1.Text >='a') and (Edit1.Text <='z')
      or (Edit1.Text >='A') and (Edit1.Text <='Z') then
  begin
  BasicSize :=0;
  Edit1.Text :=FloatToStr(1);
  Edit1.SelectAll;
  end

  else if (Edit1.Text ='') then
  begin
    BasicSize :=0;
    Edit1.SelectAll;
    CheckBox1.Checked :=False;
  end

  else if (StrToFloat(Edit1.Text) <=0) or (StrToFloat(Edit1.Text) >10000) then
  begin
    BasicSize :=0;
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入大于0小于等于10000的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    Edit1.Text :=FloatToStr(StrToFloat(Edit1.Text)/(10.0));
    Edit1.SelectAll;
    CheckBox1.Checked :=False;
  end

  else
  begin
   // CheckBox1.Checked :=true;
    BasicSize :=StrToFloat(Edit1.Text);
  end;

  if (ComboBox1.Text <> '1') and (ComboBox1.Text <> '2') and (ComboBox1.Text <> '3') and
    (ComboBox1.Text <> '4') and (ComboBox1.Text <> '5') and (ComboBox1.Text <> '6') and
    (ComboBox1.Text <> '7') and (ComboBox1.Text <> '8') and (ComboBox1.Text <> '9') and
    (ComboBox1.Text <> '10') and (ComboBox1.Text <> '11') and (ComboBox1.Text <> '12') then
  begin
    MessageBox (
    HWND (0),
    LPCTSTR ('请输入1到12之间的整数'),
    LPCTSTR ('提示'),
    UINT (MB_OK)
    );
    ComboBox1.Text := '6';
    GCDJNum :=StrToFloat(ComboBox1.Text);
    GCDJ :='等级'+FloatToStr(GCDJNum);
  end
  else
  begin
  GCDJNum :=StrToFloat(ComboBox1.Text);
  GCDJ :='等级'+FloatToStr(GCDJNum);
  end;
  
  if (RadioButton3.Checked = true) or (RadioButton4.Checked = true) then
  begin

    //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
    Data1.ADOTable1.First;
    while not Data1.ADOTable1.Eof do
    begin
      if  BasicSize <=Data1.ADOTable1.FieldByName('主参数').AsFloat then
      begin
        BasicSize1 := Data1.ADOTable1.FieldByName('主参数').AsFloat;
        Break;
      end;
      Data1.ADOTable1.Next;
    end;

    Data1.ADOTable1.First;
    if Data1.ADOTable1.Locate('主参数',BasicSize1,[loPartialKey]) then
    begin
      GCValue := Data1.ADOTable1.FieldByName(GCDJ).AsFloat;
    end;
    GCValue :=GCValue*1000;
    
    LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
  end;

  if (RadioButton5.Checked = true) or (RadioButton6.Checked = true) then
  begin
    if (StrToFloat(Edit1.Text)>500) then
    begin
      MessageBox (
      HWND (0),
      LPCTSTR ('请输入大于0小于等于500的整数'),
      LPCTSTR ('提示'),
      UINT (MB_OK)
      );
      Edit1.Text :=FloatToStr(StrToFloat(Edit1.Text)/(10.0));
      Edit1.SelectAll;
    end;

    //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
    Data1.ADOTable2.First;
    while not Data1.ADOTable2.Eof do
    begin
      if  BasicSize <=Data1.ADOTable2.FieldByName('主参数').AsFloat then
      begin
        BasicSize2 := Data1.ADOTable2.FieldByName('主参数').AsFloat;
        Break;
      end;
      Data1.ADOTable2.Next;
    end;

    Data1.ADOTable2.First;
    if Data1.ADOTable2.Locate('主参数',BasicSize2,[loPartialKey]) then
    begin
      GCValue := Data1.ADOTable2.FieldByName(GCDJ).AsFloat;
    end;
    GCValue :=GCValue*1000;
    
    LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
  end;

  if (RadioButton7.Checked = true) or (RadioButton8.Checked = true) or (RadioButton9.Checked = true) then
  begin

    //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
    Data1.ADOTable3.First;
    while not Data1.ADOTable3.Eof do
    begin
      if  BasicSize <=Data1.ADOTable3.FieldByName('主参数').AsFloat then
      begin
        BasicSize3 := Data1.ADOTable3.FieldByName('主参数').AsFloat;
        Break;
      end;
      Data1.ADOTable3.Next;
    end;

    Data1.ADOTable3.First;
    if Data1.ADOTable3.Locate('主参数',BasicSize3,[loPartialKey]) then
    begin
      GCValue := Data1.ADOTable3.FieldByName(GCDJ).AsFloat;
    end;
    GCValue :=GCValue*1000;
    
    LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
  end;

  if (RadioButton10.Checked = true) or (RadioButton11.Checked = true)
      or (RadioButton12.Checked = true) or (RadioButton13.Checked = true) then
  begin

    //确定Tabel1中的BasicSize的值，赋给BasicSize1，用于查询GCValue
    Data1.ADOTable4.First;
    while not Data1.ADOTable4.Eof do
    begin
      if  BasicSize <=Data1.ADOTable4.FieldByName('主参数').AsFloat then
      begin
        BasicSize4 := Data1.ADOTable4.FieldByName('主参数').AsFloat;
        Break;
      end;
      Data1.ADOTable4.Next;
    end;

    Data1.ADOTable4.First;
    if Data1.ADOTable4.Locate('主参数',BasicSize4,[loPartialKey]) then
    begin
      GCValue := Data1.ADOTable4.FieldByName(GCDJ).AsFloat;
    end;
    GCValue :=GCValue*1000;
    
    LB.Caption :=floatToStr(round(GCValue)/1000000)+' mm';
  end;
end;

procedure Tfrmgccx.FormCreate(Sender: TObject);
begin
  RadioButton9.Enabled :=False;
  RadioButton10.Enabled :=False;
  RadioButton11.Enabled :=False;
  RadioButton12.Enabled :=False;
  RadioButton13.Enabled :=False;
end;

procedure Tfrmgccx.FormShow(Sender: TObject);
var prj_path : string;
begin
  prj_path:=ExtractFilePath(ParamStr(0));
  {T_Tree.DatabaseName:= prj_path+'database';
  Q_Tree.DatabaseName:= prj_path+'database';
  QF_Tree.DatabaseName:= prj_path+'database';
  Query1.DatabaseName:=prj_path+'database';
  T_History.DatabaseName:=prj_path+'database';
  TreeM.DatabaseName:=prj_path+'database';
  tBook.DatabaseName:=prj_path+'database';
  tblView.DatabaseName:=prj_path+'dat';}

  data1.ADOConnection1.ConnectionString:='Provider=Microsoft.Jet.OLEDB.4.0;Data Source='+prj_path+'\DATAS\cooper.mdb;Persist Security Info=False';

  data1.ADOConnection1.Connected:=True;
  data1.ADOTable1.Open;
  data1.ADOTable2.Open;
  data1.ADOTable3.Open;
  data1.ADOTable4.Open;
end;

procedure Tfrmgccx.BitBtn1Click(Sender: TObject);
begin
  close();
end;

procedure Tfrmgccx.C1Click(Sender: TObject);
var
prj_path: string;
begin
  prj_path:=ExtractFilePath(ParamStr(0));
  ShellExecute(0,'Open',pchar(prj_path+'hlp\形位公差查询帮助文件.chm') , Nil,nil, SW_SHOWNORMAL);
end;

end.
