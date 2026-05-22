{==============================================================================

  IDE Designer Unit               
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All contents of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information
 
  $Id: te_designer.pas,v 1.10.2.1 2003/01/23 16:14:46 evgeny Exp $

===============================================================================}

unit te_designer;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, SysUtils, Classes, Controls, Forms, Menus, StdCtrls, Buttons,
  ComCtrls, CheckLst, ExtCtrls, Graphics, ToolIntf, ExptIntf, EditIntf,
  {$IFDEF KS_COMPILER6_UP}
  DesignIntf, DesignWindows, DesignEditors,
  {$ELSE}
  DsgnIntf, DsgnWnds, LibIntf,
  {$ENDIF}
  te_controls;

type

  TTeClassKind = (ckCheckBox, ckRadioButton, ckTrackBar, ckGroupBox, ckRadioGroup,
    ckScrollBar, ckListBox, ckEdit, ckComboBox, ckSpeedButton, ckSpinButton,
    ckButton, ckProgressBar, ckCustomForm, ckEngine, ckTabControl,
    ckMemo, ckSpinEdit, ckSplitter, ckMaskEdit, ckScrollBox, ckLabel, ckStringGrid,
    ckDrawGrid, ckToolbar, ckControlBar, ckStatusBar, ckHeaderControl, ckTreeView,
    ckListView, ckPanel,
    ckTabSheet, ckPageControl,
    { Last }
    ckLast);

  TTeConvertEvent = procedure (Sender: TObject; Max, Position: integer) of object;

  TTeCustomConverter = class(TComponent)
  private
    FRules: TStrings;
    FLog: TStrings;
    FOnProgress: TTeConvertEvent;
    function GetRules(Kind: TTeClassKind): string;
    procedure SetRules(Kind: TTeClassKind; const Value: string);
    procedure SetLog(const Value: TStrings);
    procedure SetOnProgress(const Value: TTeConvertEvent);
  protected
    FForm: TCustomForm;
    procedure InsertToLog(Line: string);
    procedure Progress(Max, Pos: integer);

    procedure BuildRules;
    procedure CopyExistsProperties(Source, Dest: TObject);
    procedure CopyPropertyValue(Source, Dest: TObject; APropName: string);
    procedure SetPropertyIfExists(AComp: Tcomponent; APropName: string; Value: TObject);
    function CreateCopy(AComp: TComponent; Kind: TTeClassKind): TComponent;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;

    function GetClass(ClassKind: TTeClassKind): TComponentClass; virtual;
    procedure SetAdvancedProp(AOldObject, ANewObject: TObject); virtual;

    procedure ConvertForm(Form: TCustomForm);
    property Rules[Kind: TTeClassKind]: string read GetRules write SetRules;

    property Log: TStrings read FLog write SetLog;
    property OnProgress: TTeConvertEvent read FOnProgress write SetOnProgress;
  end;

  TTeConverterClass = class of TTeCustomConverter;

  TfrmAddClass = class(TForm)
  private
    { Private declarations }
    Label1: TLabel;
    Button1: TButton;
    Button2: TButton;
  public
    { Public declarations }
    cbClassList: TComboBox;
    constructor CreateNew(AOwner: TComponent; Dummy: Integer = 0); override;
  end;

  TTeTabControlEditor = class(TDefaultEditor)
  public
    procedure ExecuteVerb (Index: Integer); override;
    function GetVerb (Index: Integer): String; override;
    function GetVerbCount: Integer; override;
  end;

  TfrmConvertForm = class(TForm)
    Pages: TNotebook;
    Image1: TImage;
    lbWelcome: TLabel;
    Bevel1: TBevel;
    btnCancel: TBitBtn;
    btnNext: TBitBtn;
    btnBack: TBitBtn;
    Label2: TLabel;
    Label3: TLabel;
    Bevel2: TBevel;
    Panel1: TPanel;
    lbWText: TLabel;
    lbWarning: TLabel;
    Label1: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    GroupBox1: TGroupBox;
    lbOldComps: TListBox;
    btnAddOld: TButton;
    Bevel3: TBevel;
    Panel2: TPanel;
    Label6: TLabel;
    LogMemo: TMemo;
    btnClose: TBitBtn;
    btnStart: TBitBtn;
    CProg: TProgressBar;
    lbNewComps: TCheckListBox;
    Label7: TLabel;
    procedure PagesPageChanged(Sender: TObject);
    procedure btnNextClick(Sender: TObject);
    procedure btnBackClick(Sender: TObject);
    procedure btnStartClick(Sender: TObject);
    procedure lbNewCompsClick(Sender: TObject);
    procedure btnAddOldClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    { Private declarations }
    FConvertClass: TTeConverterClass;
    FConvertName: string;
    FConvertForm: TCustomForm;
    FConverter: TTeCustomConverter;
    procedure SetConvertClass(const Value: TTeConverterClass);
    procedure SetConvertName(const Value: string);
    procedure SetConvertForm(const Value: TCustomForm);
    procedure DoConvert(Sender: TObject; Max, Pos: integer);
    procedure BuildNewCompsList;
    procedure BuildOldCompsList;
  protected
    procedure Convert; virtual;
  public
    { Public declarations }
    constructor CreateForm; virtual;
    destructor Destroy; override;

    procedure InitConverter; virtual;

    property ConvertClass: TTeConverterClass read FConvertClass write SetConvertClass;
    property ConvertForm: TCustomForm read FConvertForm write SetConvertForm;
    property ConvertName: string read FConvertName write SetConvertName;
    property Converter: TTeCustomConverter read FConverter;
  end;

  TfrmMenuDesignerForm = class(TDesignWindow)
    Timer1: TTimer;
    Pages: TPageControl;
    TabSheet1: TTabSheet;
    TabSheet2: TTabSheet;
    Panel2: TPanel;
    TreeItems: TTreeView;
    ItemsName: TPanel;
    Panel4: TPanel;
    Panel1: TPanel;
    Panel5: TPanel;
    Bevel1: TBevel;
    btnInsert: TButton;
    btnSubMenu: TButton;
    btnDelete: TButton;
    btnUp: TBitBtn;
    btnDown: TBitBtn;
    Label1: TLabel;
    cbSource: TComboBox;
    Button1: TButton;
    BtnConvert: TButton;
    Label2: TLabel;
    Memo1: TMemo;
    procedure btnInsertClick(Sender: TObject);
    procedure TreeItemsClick(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormCreate(Sender: TObject);
    procedure btnSubMenuClick(Sender: TObject);
    procedure TreeItemsEdited(Sender: TObject; Node: TTreeNode;
      var S: String);
    procedure btnDeleteClick(Sender: TObject);
    procedure btnUpClick(Sender: TObject);
    procedure btnDownClick(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure TreeItemsDblClick(Sender: TObject);
    procedure BtnConvertClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure PagesChange(Sender: TObject);
  private
    { Private declarations }
    FParentComponent: TComponent;
    FRootItem: TTeCustomItem;
    FSelRootItem: TTeCustomItem;
    FSelItem: TTeCustomItem;
    procedure RebuildTreeItem;
    procedure InsertNewItem;
    procedure DoItemsChange(Sender: TObject);
  protected
    procedure Notification (AComponent: TComponent; Operation: TOperation); override;
    function UniqueName (Component: TComponent): String; override;
  public
    { Public declarations }
    constructor CreateForm; virtual;
    destructor Destroy; override;
  end;

{ IDE Editors }

  TTeMenuBarEditor = class(TDefaultEditor)
  public
    procedure Edit; override;
    procedure ExecuteVerb (Index: Integer); override;
    function GetVerb (Index: Integer): String; override;
    function GetVerbCount: Integer; override;
  end;

  TTePopupMenuEditor = class(TDefaultEditor)
  public
    procedure Edit; override;
    procedure ExecuteVerb (Index: Integer); override;
    function GetVerb (Index: Integer): String; override;
    function GetVerbCount: Integer; override;
  end;

  TTeItemsPropertyEditor = class(TStringProperty)
  public
    procedure Edit; override;
    function GetAttributes: TPropertyAttributes; override;
    function GetValue: String; override;
  end;

var
  frmConvertForm: TfrmConvertForm;
  frmAddClass: TfrmAddClass;
  frmMenuDesignerForm: TfrmMenuDesignerForm;

{ Rules list routines }

function GetClassCount(Rules: string): integer;
function GetClassName(Rules: string; Index: integer): string;
procedure AddClassName(var Rules: string; NewClassName: string);
function AddClassName2(Rules: string; NewClassName: string): string;

procedure Register;

implementation {===============================================================}

uses TypInfo, Dialogs;

const
  SDefaultMenuItemName = 'CustomItem';
  
type

  THackControl = class(TControl);

procedure Register;
begin
  RegisterPropertyEditor(TypeInfo(TTeCustomItem), nil, 'Items', TTeItemsPropertyEditor);

  RegisterComponentEditor(TTeCustomTabControl, TTeTabControlEditor);
  RegisterComponentEditor(TTeCustomTabSheet, TTeTabControlEditor);
end;

function GetClassCount(Rules: string): integer;
begin
  Result := 0;
  while Rules <> '' do
  begin
    GetToken(Rules);
    Inc(Result);
  end;
end;

function GetClassName(Rules: string; Index: integer): string;
var
  Token: string;
  i: integer;
begin
  i := 0;
  while Rules <> '' do
  begin
    Token := GetToken(Rules);
    if i = Index then
    begin
      Result := Token;
      Exit;
    end;
    Inc(i);
  end;
  Result := '';
end;

procedure AddClassName(var Rules: string; NewClassName: string);
begin
  if (Rules <> '') and (Rules[Length(Rules)] <> ';') then
    Rules := Rules + ';';

  Rules := Rules + NewClassName;
end;

function AddClassName2(Rules: string; NewClassName: string): string;
begin
  if (Rules <> '') and (Rules[Length(Rules)] <> ';') then
    Rules := Rules + ';';

  Result := Rules + NewClassName;
end;

{ TTeCustomConverter ==========================================================}

constructor TTeCustomConverter.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  FRules := TStringList.Create;

  BuildRules;
end;

destructor TTeCustomConverter.Destroy;
begin
  FRules.Free;
  inherited Destroy;
end;

procedure TTeCustomConverter.BuildRules;
begin
  Rules[ckButton] := 'TButton;TBitBtn;TRzButton;TspSkinButton;TbsSkinButton';
  Rules[ckCheckBox] := 'TCheckBox;TRzCheckBox;TspSkinCheckRadioBox;TbsSkinCheckRadioBox';
  Rules[ckRadioButton] := 'TRadioButton;TRzRadioButton';
  Rules[ckTrackBar] := 'TTackBar;TRxSlider;TRzTrackBar;TspSkinTrackBar;TbsSkinTrackBar';
  Rules[ckGroupBox] := 'TGroupBox';
  Rules[ckRadioGroup] := 'TRadioGroup;TRzRadioGroup';
  Rules[ckScrollBar] := 'TScrollBar;TspSkinScrollBar;TbsSkinScrollBar';
  Rules[ckListBox] := 'TListBox;TCheckListBox;TRzListBox;TRzCheckList;TspSkinListBox;TspSkinCheckListBox;TbsSkinListBox;TbsSkinCheckListBox';
  Rules[ckEdit] := 'TEdit;TMaskEdit;TRzEdit;TspSkinEdit;TbsSkinEdit';
  Rules[ckComboBox] := 'TComboBox;TRzComboBox;TspSkinComboBox;TbsSkinComboBox';
  Rules[ckSpeedButton] := 'TSpeedButton;TToolButton;TRxSpeedButton;TRzToolbarButton;TspSkinMenuButton;TbsSkinMenuButton';
  Rules[ckSpinButton] := 'TSpinButton;TUpDown;TRxSpinButton;TspSkinUpDown;TbsSkinUpDown';
  Rules[ckProgressBar] := 'TGauge;TProgressBar;TRzProgressBar;TspSkinGauge;TspFrameSkinGauge;TbsFrameSkinGauge';
  Rules[ckTabControl] := 'TTabControl;TspSkinTabControl;TbsSkinTabControl';
  Rules[ckMemo] := 'TMemo;TRzMemo;TRzMemo;TspSkinMemo;TbsSkinMemo';
  Rules[ckSpinEdit] := 'TSpinEdit;TRxSpinEdit;TspSkinSpinEdit;TbsSkinSpinEdit';
  Rules[ckSplitter] := 'TSplitter;TspSkinSplitter;TbsSkinSplitter';
  Rules[ckMaskEdit] := 'TMaskEdit';
  Rules[ckScrollBox] := 'TScrollBox;TspSkinScrollBox;TbsSkinScrollBox';
  Rules[ckLabel] := 'TLabel;TRzLabel;TRzLabel;TspSkinLabel;TspSkinStdLabel;TbsSkinStdLabel';
  Rules[ckStringGrid] := 'TStringGrid;TspSkinStringGrid;TbsSkinStringGrid';
  Rules[ckDrawGrid] := 'TDrawGrid;TspSkinDrawGrid;TbsSkinDrawGrid';
  Rules[ckToolbar] := 'TToolbar;TRxToolbar';
  Rules[ckControlBar] := 'TControlBar;TspSkinControlBar;TbsSkinControlBar';
  Rules[ckStatusBar] := 'TStatusBar';
  Rules[ckHeaderControl] := 'THeaderControl';
  Rules[ckTreeView] := 'TTreeView;TspTreeView;TbsTreeView';
  Rules[ckListView] := 'TListView;TspListView;TbsListView';
  Rules[ckPanel] := 'TPanel';
  Rules[ckTabSheet] := 'TTabSheet';
  Rules[ckPageControl] := 'TPageControl';
end;

procedure TTeCustomConverter.ConvertForm(Form: TCustomForm);
var
  i, r, c: integer;
  Comp, NewComp: TComponent;
  ClassList: string;
begin
  if Form = nil then Exit;
  FForm := Form;

  if FLog <> nil then FLog.Clear;

  InsertToLog(Format('Convertion "%s" form, please wait...', [Form.Name]));
  InsertToLog('');
  { Call OnProgress }
  Progress(Form.ComponentCount, 0);
  { Convert form }
  for i := 0 to Form.ComponentCount - 1 do
  begin
    Comp := Form.Components[i];
    { Search in Rules  }
    for r := 0 to Integer(ckLast) - 1 do
    begin
      ClassList := Rules[TTeClassKind(r)];

      NewComp := nil;

      for c := 0 to GetClassCount(ClassList) - 1 do
        if LowerCase(Comp.ClassName) = LowerCase(GetClassName(ClassList, c)) then
        begin
          { Crete New Component }
          try
            NewComp := CreateCopy(Comp, TTeClassKind(r));
          except
            InsertToLog('!!! Convertion error : '+Comp.Name);
          end;
          Break;
        end;

      if NewComp <> nil then Break;
    end;
    { Call OnProgress }
    Progress(Form.ComponentCount, i);
  end;
  Progress(Form.ComponentCount, Form.ComponentCount);

  InsertToLog('Setting pages!');
  for i := 0 to Form.ComponentCount - 1 do
  begin
    Comp := Form.Components[i];
    if (Comp is TTeCustomTabSheet) then
    begin
      ((Comp as TControl).Parent as TTeCustomTabControl).AddPage(Comp as TTeCustomTabSheet);
    end;
  end;

  InsertToLog('Convertion complete!');
end;

function TTeCustomConverter.CreateCopy(AComp: TComponent; Kind: TTeClassKind): TComponent;
var
  C, OldName: string;
  CompClass: TComponentClass;
  CompIndex: integer;
begin
  CompClass := GetClass(Kind);

  if CompClass <> nil then
  begin
    { Save and change name }
    OldName := AComp.Name;
    CompIndex := AComp.ComponentIndex;
    { Create new }
    Result := CompClass.Create(AComp.Owner);
    Result.Name := OldName+'_T';
    Result.ComponentIndex := CompIndex;
    InsertToLog(Format('Create copy of "%s" component.', [Result.Name]));
    { Set Control property }
    if AComp is TControl then
    begin
      TControl(Result).Parent := TControl(AComp).Parent;
    end;
    { Copy properties }
    InsertToLog(Format('Copy "%s"''s properties.', [Result.Name]));
    try
      CopyExistsProperties(AComp, Result);
    except
      InsertToLog('!!! Properties convertion error : '+AComp.Name);
    end;
    { Set Advanced Properties }
    SetAdvancedProp(AComp, Result);
    { Delete old }
    if AComp is TControl then
      C := THackControl(AComp).Caption;
    AComp.Free;
    { Rename }
    if Result is TControl then
    begin
      Result.Name := OldName;
      THackControl(Result).Caption := C;
    end
    else
      Result.Name := OldName;
  end;
  InsertToLog('');
end;

{ Virtual }

function TTeCustomConverter.GetClass(ClassKind: TTeClassKind): TComponentClass;
begin
  case ClassKind of
    ckButton: Result := TTeCustomButton;
    ckEdit: Result := TTeCustomEdit;
    ckGroupBox: Result := TTeCustomGroupBox;
  else
    Result := nil;
  end;
end;

procedure TTeCustomConverter.SetAdvancedProp(AOldObject, ANewObject: TObject);
var
  i: integer;
  Control: TControl;
begin
  { Set Parent on children controls }
  if (FForm <> nil) and (AOldObject is TWinControl) and (csAcceptsControls in (AOldObject as TWinControl).ControlStyle) then
  begin
    for i := 0 to FForm.ComponentCount - 1 do
      if FForm.Components[i] is TControl then
      begin
        Control := FForm.Components[i] as TControl;
        if Control.Parent = (AOldObject as TControl) then
          THackControl(Control).SetParent(ANewObject as TWinControl);
      end;
  end;

  { Set Parent on tab controls }
  InsertToLog('Tab controls');
  if (FForm <> nil) and (AOldObject is TWinControl) and (AOldObject is TCustomTabControl) then
  begin
    for i := 0 to FForm.ComponentCount - 1 do
      if FForm.Components[i] is TControl then
      begin
        Control := FForm.Components[i] as TControl;
        if Control.Parent = (AOldObject as TControl) then
        begin
          THackControl(Control).SetParent(ANewObject as TWinControl);
          //(ANewObject as TSeCustomTabControl).AddPage(Control as TSeCustomTabSheet);
        end;
      end;
  end;
	
  { For Button }
  if (ANewObject is TTeCustomButton) and (AOldObject is TWinControl) then
  begin
    (ANewObject as TTeCustomButton).Caption := THackControl(AOldObject).Caption;
  end;

  { GroupBox }
  if (ANewObject is TTeCustomGroupBox) and (AOldObject is TGroupBox) then
    TTeCustomGroupBox(ANewObject).Caption := TGroupBox(AOldObject).Caption;

  { ComboBox }
  if (ANewObject is TTeCustomComboBox) and (AOldObject is TComboBox) then
  begin
    case TComboBox(AOldObject).Style of
      csDropDown: TTeCustomComboBox(ANewObject).ComboStyle := kcsDropDown;
      csDropDownList: TTeCustomComboBox(ANewObject).ComboStyle := kcsDropDownList;
    end;
  end;

  { Panel }
  if (ANewObject is TTeCustomPanel) and (AOldObject is TPanel) then
  begin
    TTeCustomPanel(ANewObject).ShowCaption := false;
  end;

  { StatusBar }
  if (ANewObject is TTeCustomStatusBar) and (AOldObject is TStatusBar) then
  begin
    if TTeCustomStatusBar(ANewObject).Panels.Count > 0 then
      TTeCustomStatusBar(ANewObject).Panels[TTeCustomStatusBar(ANewObject).Panels.Count - 1].StretchPriority := 100;
  end;

  { Header }
  if (ANewObject is TTeCustomHeaderControl) and (AOldObject is THeaderControl) then
  begin
    TControl(ANewObject).BoundsRect := TControl(AOldObject).BoundsRect;
  end;

  { ProgressBar }
  if (ANewObject is TTeCustomProgressBar) and (AOldObject is TProgressBar) then
  begin
    TTeCustomProgressBar(ANewObject).Orientation := TTeBarOrientation(TProgressBar(AOldObject).Orientation);
  end;

  { Splitter }
  if (ANewObject is TTeCustomSplitter) and (AOldObject is TSplitter) then
  begin
    if TSplitter(AOldObject).Align in [alTop, alBottom] then
      TTeCustomSplitter(ANewObject).Height := TSplitter(AOldObject).Height; 
  end;
end;

{ Internal routines }

procedure TTeCustomConverter.CopyPropertyValue(Source, Dest: TObject; APropName: string);
var
  SourcePropInfo: PPropInfo;
  DestPropInfo: PPropInfo;
  SourceObjectProp, DestObjectProp: TObject;
  SetPropValue: string;
begin
  { Copy property PropName value from source to dest }
  SourcePropInfo := GetPropInfo(Source.ClassInfo, APropName);
  if SourcePropInfo = nil then Exit;

  DestPropInfo := GetPropInfo(Dest.ClassInfo, APropName);
  if DestPropInfo = nil then Exit;

  if Dest is TComponent then
    InsertToLog(Format('  Copy "%s.%s" property.', [TComponent(Dest).Name, APropName]));

  { Copy }
  if SourcePropInfo^.PropType^.Kind = DestPropInfo^.PropType^.Kind then
  begin
    case SourcePropInfo^.PropType^.Kind of
      tkInteger, tkChar: SetOrdProp(Dest, DestPropInfo, GetOrdProp(Source, SourcePropInfo));
      tkFloat: SetFloatProp(Dest, DestPropInfo, GetFloatProp(Source, SourcePropInfo));
      tkString, tkWString, tkWChar, tkLString: SetStrProp(Dest, DestPropInfo, GetStrProp(Source, SourcePropInfo));
      {$IFDEF KS_COMPILER5_UP}
      tkEnumeration: SetEnumProp(Dest, DestPropInfo, GetEnumProp(Source, SourcePropInfo));
      tkSet: begin
        SetPropValue := '['+GetSetProp(Source, SourcePropInfo)+']';
        SetSetProp(Dest, DestPropInfo, SetPropValue);
      end;
      tkClass: begin
        SourceObjectProp := GetObjectProp(Source, SourcePropInfo);
        DestObjectProp := GetObjectProp(Dest, DestPropInfo);

        if (SourceObjectProp is TPersistent) and not (SourceObjectProp is TComponent) then
          TPersistent(DestObjectProp).Assign(TPersistent(SourceObjectProp))
        else
          SetObjectProp(Dest, DestPropInfo, SourceObjectProp);
      end;
      {$ELSE}
      tkClass: begin
        SourceObjectProp := TObject(GetOrdProp(Source, SourcePropInfo));
        DestObjectProp := TObject(GetOrdProp(Dest, DestPropInfo));

        if (SourceObjectProp is TPersistent) and not (SourceObjectProp is TComponent) then
          TPersistent(DestObjectProp).Assign(TPersistent(SourceObjectProp))
        else
          SetOrdProp(Dest, DestPropInfo, Integer(SourceObjectProp));
      end;
      {$ENDIF}
      tkMethod: SetMethodProp(Dest, DestPropInfo, GetMethodProp(Source, SourcePropInfo));
    end;
  end;
end;

procedure TTeCustomConverter.CopyExistsProperties(Source, Dest: TObject);
var
  PropList: PPropList;
  ClassTypeInfo: PTypeInfo;
  ClassTypeData: PTypeData;
  i: integer;
begin
  if (Source = nil) or (Dest = nil) then Exit;

  ClassTypeInfo := Dest.ClassInfo;
  ClassTypeData := GetTypeData(ClassTypeInfo);

  if ClassTypeData.PropCount <> 0 then
  begin
    { allocate the memory needed to hold the references to the TPropInfo }
    { structures on the number of properties. }
    GetMem(PropList, SizeOf(PPropInfo) * ClassTypeData.PropCount);
    try
      { fill PropList with the pointer references to the TPropInfo structures }
      GetPropInfos(Dest.ClassInfo, PropList);
      for i := 0 to ClassTypeData.PropCount - 1 do
      begin
        if LowerCase(PropList[i]^.Name) = 'name' then Continue;
        if LowerCase(PropList[i]^.Name) = 'orientation' then Continue;
        { Copy property's value }
        CopyPropertyValue(Source, Dest, PropList[i]^.Name);
      end;
    finally
      FreeMem(PropList, SizeOf(PPropInfo) * ClassTypeData.PropCount);
    end;
  end;
end;

procedure TTeCustomConverter.SetPropertyIfExists(AComp: Tcomponent;
  APropName: string; Value: TObject);
var
  PropInfo: PPropInfo;
begin
  PropInfo := GetPropInfo(AComp.ClassInfo, APropName);
  if PropInfo = nil then Exit;

  SetOrdProp(AComp, PropInfo, Integer(Value));
end;

procedure TTeCustomConverter.InsertToLog(Line: string);
begin
  if FLog <> nil then
    FLog.Add(Line);
end;

procedure TTeCustomConverter.Progress(Max, Pos: integer);
begin
  if Assigned(FOnProgress) then
  begin
    FOnProgress(Self, Max, Pos);
    Application.ProcessMessages;
  end;
end;

{ Properties }

function TTeCustomConverter.GetRules(Kind: TTeClassKind): string;
begin
  Result := FRules.Values['Ident'+IntToStr(Integer(Kind))];
end;

procedure TTeCustomConverter.SetRules(Kind: TTeClassKind; const Value: string);
begin
  FRules.Values['Ident'+IntToStr(Integer(Kind))] := Value;
end;

procedure TTeCustomConverter.SetLog(const Value: TStrings);
begin
  FLog := Value;
end;

procedure TTeCustomConverter.SetOnProgress(const Value: TTeConvertEvent);
begin
  FOnProgress := Value;
end;


{ TfrmAddClass ================================================================}


constructor TfrmAddClass.CreateNew(AOwner: TComponent; Dummy: Integer = 0);
var
  m, c: integer;
begin
  inherited;

  BorderStyle := bsDialog;

  Label1 := TLabel.Create(Self);
  with Label1 do
  begin
    Parent := Self;
    Visible := true;
    Left := 8;
    Top := 10;
    Width := 60;
    Height := 13;
    Caption := 'Select class:';
  end;

  cbClassList := TComboBox.Create(Self);
  with cbClassList do
  begin
    Parent := Self;
    Visible := true;
    Left := 8;
    Top := 30;
    Width := 249;
    Height := 21;
    Style := csDropDownList;
    ItemHeight := 13;
    TabOrder := 0;
  end;

  Button1 := TButton.Create(Self);
  with Button1 do
  begin
    Parent := Self;
    Visible := true;

    Left := 135;
    Top := 72;
    Width := 75;
    Height := 25;
    Caption := 'Cancel';
    ModalResult := 2;
    TabOrder := 1;
  end;

  Button2 := TButton.Create(Self);
  with Button2 do
  begin
    Parent := Self;
    Visible := true;

    Left := 55;
    Top := 72;
    Width := 75;
    Height := 25;
    Caption := 'OK';
    ModalResult := 1;
    TabOrder := 2;
  end;

  if ToolServices = nil then Exit;

  cbClassList.Items.Clear;
  for m := 0 to ToolServices.GetModuleCount-1 do
    for c := 0 to ToolServices.GetComponentCount(m)- 1 do
      cbClassList.Items.Add(ToolServices.GetComponentName(m, c));
end;


{ TTeTabControlEditor =========================================================}


procedure TTeTabControlEditor.ExecuteVerb(Index: Integer);
var
  TC: TTeCustomTabControl;
  Page: TTeCustomTabSheet;
begin
  if Component is TTeCustomTabSheet then
  begin
    TC := (Component as TTeCustomTabSheet).TabControl;
  end
  else
    if not (Component is TTeCustomTabControl) then
      Exit
    else
      TC := Component as TTeCustomTabControl;

  case Index of
    0: { New Page }
      begin
        TC.Tabs.Add('New Tab ' + IntToStr(TC.Tabs.Count));
        if TC.UsePages then
          TC.SetActivePageIndex(TC.PageCount - 1);
      end;
    1: { Delete Page }
      begin
        if TC.UsePages then
        begin
          Page := TC.Pages[TC.ActivePageIndex];
          if Page <> nil then
            Page.Free;

          TC.TabIndex := 0;
        end;
      end;
  end;
end;

function TTeTabControlEditor.GetVerbCount: Integer;
begin
  Result := 2;
end;

function TTeTabControlEditor.GetVerb(Index: Integer): String;
begin
  case Index of
    0: Result := 'New Tab';
    1: Result := 'Delete Tab';
  else
    Result := '';
  end;
end;

{ TfrmConvertForm =============================================================}


const ConvertFormRes: array [0..30659] of byte = (
$FF,$A,$0,$54,$46,$52,$4D,$43,$4F,$4E,$56,$45,$52,$54,$46,$4F,$52,$4D,$0,$30,$10,$AA,$77,$0,$0,$54,$50,$46,$30,$F,$54,$66,$72,$6D,$43,$6F,$6E,$76,$65,$72,
$74,$46,$6F,$72,$6D,$E,$66,$72,$6D,$43,$6F,$6E,$76,$65,$72,$74,$46,$6F,$72,$6D,$4,$4C,$65,$66,$74,$3,$56,$1,$3,$54,$6F,$70,$3,$BB,$0,$B,$42,$6F,$72,$64,
$65,$72,$53,$74,$79,$6C,$65,$7,$8,$62,$73,$44,$69,$61,$6C,$6F,$67,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$16,$43,$6F,$6D,$70,$6F,$6E,$65,$6E,$74,$27,$73,$20,$43,
$6F,$6E,$76,$65,$72,$74,$65,$72,$20,$C,$43,$6C,$69,$65,$6E,$74,$48,$65,$69,$67,$68,$74,$3,$74,$1,$B,$43,$6C,$69,$65,$6E,$74,$57,$69,$64,$74,$68,$3,$DA,$1,
$5,$43,$6F,$6C,$6F,$72,$7,$9,$63,$6C,$42,$74,$6E,$46,$61,$63,$65,$C,$46,$6F,$6E,$74,$2E,$43,$68,$61,$72,$73,$65,$74,$7,$F,$52,$55,$53,$53,$49,$41,$4E,$5F,
$43,$48,$41,$52,$53,$45,$54,$A,$46,$6F,$6E,$74,$2E,$43,$6F,$6C,$6F,$72,$7,$C,$63,$6C,$57,$69,$6E,$64,$6F,$77,$54,$65,$78,$74,$B,$46,$6F,$6E,$74,$2E,$48,$65,
$69,$67,$68,$74,$2,$F5,$9,$46,$6F,$6E,$74,$2E,$4E,$61,$6D,$65,$6,$5,$41,$72,$69,$61,$6C,$A,$46,$6F,$6E,$74,$2E,$53,$74,$79,$6C,$65,$B,$0,$E,$4F,$6C,$64,
$43,$72,$65,$61,$74,$65,$4F,$72,$64,$65,$72,$8,$8,$50,$6F,$73,$69,$74,$69,$6F,$6E,$7,$E,$70,$6F,$53,$63,$72,$65,$65,$6E,$43,$65,$6E,$74,$65,$72,$8,$4F,$6E,
$43,$72,$65,$61,$74,$65,$7,$A,$46,$6F,$72,$6D,$43,$72,$65,$61,$74,$65,$D,$50,$69,$78,$65,$6C,$73,$50,$65,$72,$49,$6E,$63,$68,$2,$60,$A,$54,$65,$78,$74,$48,
$65,$69,$67,$68,$74,$2,$E,$0,$6,$54,$4C,$61,$62,$65,$6C,$6,$4C,$61,$62,$65,$6C,$33,$4,$4C,$65,$66,$74,$2,$9,$3,$54,$6F,$70,$3,$4A,$1,$5,$57,$69,$64,
$74,$68,$2,$60,$6,$48,$65,$69,$67,$68,$74,$2,$1C,$9,$41,$6C,$69,$67,$6E,$6D,$65,$6E,$74,$7,$8,$74,$61,$43,$65,$6E,$74,$65,$72,$7,$41,$6E,$63,$68,$6F,$72,
$73,$B,$6,$61,$6B,$4C,$65,$66,$74,$8,$61,$6B,$42,$6F,$74,$74,$6F,$6D,$0,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$21,$28,$63,$29,$20,$4B,$53,$20,$44,$65,$76,$65,
$6C,$6F,$70,$6D,$65,$6E,$74,$D,$A,$77,$77,$77,$2E,$6B,$73,$64,$65,$76,$2E,$63,$6F,$6D,$C,$46,$6F,$6E,$74,$2E,$43,$68,$61,$72,$73,$65,$74,$7,$F,$52,$55,$53,
$53,$49,$41,$4E,$5F,$43,$48,$41,$52,$53,$45,$54,$A,$46,$6F,$6E,$74,$2E,$43,$6F,$6C,$6F,$72,$7,$8,$63,$6C,$57,$69,$6E,$64,$6F,$77,$B,$46,$6F,$6E,$74,$2E,$48,
$65,$69,$67,$68,$74,$2,$F5,$9,$46,$6F,$6E,$74,$2E,$4E,$61,$6D,$65,$6,$5,$41,$72,$69,$61,$6C,$A,$46,$6F,$6E,$74,$2E,$53,$74,$79,$6C,$65,$B,$0,$A,$50,$61,
$72,$65,$6E,$74,$46,$6F,$6E,$74,$8,$0,$0,$6,$54,$42,$65,$76,$65,$6C,$6,$42,$65,$76,$65,$6C,$31,$4,$4C,$65,$66,$74,$2,$0,$3,$54,$6F,$70,$3,$37,$1,$5,
$57,$69,$64,$74,$68,$3,$DA,$1,$6,$48,$65,$69,$67,$68,$74,$2,$6,$5,$41,$6C,$69,$67,$6E,$7,$5,$61,$6C,$54,$6F,$70,$5,$53,$68,$61,$70,$65,$7,$9,$62,$73,
$54,$6F,$70,$4C,$69,$6E,$65,$0,$0,$6,$54,$4C,$61,$62,$65,$6C,$6,$4C,$61,$62,$65,$6C,$32,$4,$4C,$65,$66,$74,$2,$8,$3,$54,$6F,$70,$3,$49,$1,$5,$57,$69,
$64,$74,$68,$2,$60,$6,$48,$65,$69,$67,$68,$74,$2,$1C,$9,$41,$6C,$69,$67,$6E,$6D,$65,$6E,$74,$7,$8,$74,$61,$43,$65,$6E,$74,$65,$72,$7,$41,$6E,$63,$68,$6F,
$72,$73,$B,$6,$61,$6B,$4C,$65,$66,$74,$8,$61,$6B,$42,$6F,$74,$74,$6F,$6D,$0,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$21,$28,$63,$29,$20,$4B,$53,$20,$44,$65,$76,
$65,$6C,$6F,$70,$6D,$65,$6E,$74,$D,$A,$77,$77,$77,$2E,$6B,$73,$64,$65,$76,$2E,$63,$6F,$6D,$C,$46,$6F,$6E,$74,$2E,$43,$68,$61,$72,$73,$65,$74,$7,$F,$52,$55,
$53,$53,$49,$41,$4E,$5F,$43,$48,$41,$52,$53,$45,$54,$A,$46,$6F,$6E,$74,$2E,$43,$6F,$6C,$6F,$72,$7,$B,$63,$6C,$42,$74,$6E,$53,$68,$61,$64,$6F,$77,$B,$46,$6F,
$6E,$74,$2E,$48,$65,$69,$67,$68,$74,$2,$F5,$9,$46,$6F,$6E,$74,$2E,$4E,$61,$6D,$65,$6,$5,$41,$72,$69,$61,$6C,$A,$46,$6F,$6E,$74,$2E,$53,$74,$79,$6C,$65,$B,
$0,$A,$50,$61,$72,$65,$6E,$74,$46,$6F,$6E,$74,$8,$B,$54,$72,$61,$6E,$73,$70,$61,$72,$65,$6E,$74,$9,$0,$0,$9,$54,$4E,$6F,$74,$65,$62,$6F,$6F,$6B,$5,$50,
$61,$67,$65,$73,$4,$4C,$65,$66,$74,$2,$0,$3,$54,$6F,$70,$2,$0,$5,$57,$69,$64,$74,$68,$3,$DA,$1,$6,$48,$65,$69,$67,$68,$74,$3,$37,$1,$5,$41,$6C,$69,
$67,$6E,$7,$5,$61,$6C,$54,$6F,$70,$5,$43,$6F,$6C,$6F,$72,$7,$7,$63,$6C,$57,$68,$69,$74,$65,$B,$50,$61,$72,$65,$6E,$74,$43,$6F,$6C,$6F,$72,$8,$8,$54,$61,
$62,$4F,$72,$64,$65,$72,$2,$0,$D,$4F,$6E,$50,$61,$67,$65,$43,$68,$61,$6E,$67,$65,$64,$7,$10,$50,$61,$67,$65,$73,$50,$61,$67,$65,$43,$68,$61,$6E,$67,$65,$64,
$0,$5,$54,$50,$61,$67,$65,$0,$4,$4C,$65,$66,$74,$2,$0,$3,$54,$6F,$70,$2,$0,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$7,$57,$65,$6C,$63,$6F,$6D,$65,$0,$6,
$54,$49,$6D,$61,$67,$65,$6,$49,$6D,$61,$67,$65,$31,$4,$4C,$65,$66,$74,$2,$0,$3,$54,$6F,$70,$2,$0,$5,$57,$69,$64,$74,$68,$3,$A4,$0,$6,$48,$65,$69,$67,
$68,$74,$3,$37,$1,$5,$41,$6C,$69,$67,$6E,$7,$6,$61,$6C,$4C,$65,$66,$74,$8,$41,$75,$74,$6F,$53,$69,$7A,$65,$9,$C,$50,$69,$63,$74,$75,$72,$65,$2E,$44,$61,
$74,$61,$A,$8A,$67,$0,$0,$7,$54,$42,$69,$74,$6D,$61,$70,$7E,$67,$0,$0,$42,$4D,$7E,$67,$0,$0,$0,$0,$0,$0,$76,$0,$0,$0,$28,$0,$0,$0,$A4,$0,$0,
$0,$3A,$1,$0,$0,$1,$0,$4,$0,$0,$0,$0,$0,$8,$67,$0,$0,$C4,$E,$0,$0,$C4,$E,$0,$0,$10,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$39,$0,$0,
$0,$84,$0,$0,$0,$84,$84,$84,$0,$A5,$A5,$A5,$0,$C6,$C6,$C6,$0,$FF,$FF,$FF,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,
$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$0,$0,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$0,$0,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$22,$0,$0,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,
$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$22,$0,$0,$22,$20,$20,$20,$20,$20,$22,
$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$22,$22,$22,$0,$0,$22,$2,$2,
$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$22,$22,$22,$0,
$0,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$22,
$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,
$2,$2,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,
$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,
$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$22,$22,$22,
$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,
$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,
$0,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$0,$0,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,
$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,
$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,
$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$22,$22,$22,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$22,$22,$22,$22,
$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,
$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,
$0,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$0,$0,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$22,$22,$22,$22,
$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,
$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,
$0,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$0,$0,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$0,$0,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$0,$0,$20,$20,$20,$20,$22,$22,$22,
$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$0,
$0,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,
$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,
$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,
$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$22,
$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$2,$2,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$20,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,
$22,$22,$22,$22,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,
$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,
$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,
$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,
$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,
$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,
$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,
$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$22,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$22,$22,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,
$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,
$0,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$0,$0,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$0,$0,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,
$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,
$0,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$0,$0,$2,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,
$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,
$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,
$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,
$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,
$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$20,$20,$20,$20,$20,$20,$22,$22,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,
$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,
$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,
$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,
$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,
$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,
$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,
$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,
$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$2,$2,$2,$2,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$62,$22,$22,$22,$22,$22,
$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$62,$22,
$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,
$66,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,
$66,$66,$66,$66,$66,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$26,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,
$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$6,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$26,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$0,
$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$6,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$26,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$66,$66,$0,$0,$0,$0,$0,$0,$0,$3,$45,$56,$60,$0,$0,$0,$4,$63,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$6,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$20,$20,$35,$66,$65,$56,$50,$20,$20,$23,$66,$66,$63,$20,$20,$20,$20,$20,$20,$20,$20,$20,$26,$66,$62,$22,$22,$22,$22,$22,
$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$46,$65,$43,$46,$53,$0,$0,$0,$55,$33,$60,$35,$65,$30,$0,$0,$0,$0,$0,$0,$0,$6,$66,$62,$22,
$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$46,$66,$30,$20,$20,$20,$20,$20,$46,$40,$23,$60,$20,$23,$66,$40,$20,$20,$20,$20,$20,$20,
$26,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$2,$2,$2,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$6,$43,$60,$0,$0,$0,$0,$0,$36,$53,$36,$33,$60,$0,$0,$3,$56,$53,$0,
$0,$0,$0,$0,$6,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$20,$20,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$63,$25,$30,$20,$20,$20,$20,$24,$63,$35,$63,$23,$63,$20,$20,
$20,$20,$36,$64,$20,$20,$20,$20,$26,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,
$22,$22,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$5,$40,$36,$0,$0,$0,$0,$3,$65,$3,$63,$3,
$66,$66,$63,$0,$0,$0,$0,$34,$65,$30,$0,$0,$6,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$26,$20,$53,$20,$20,$20,$20,$56,
$30,$35,$23,$56,$66,$66,$66,$65,$30,$20,$20,$20,$23,$56,$50,$20,$26,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$44,$0,$63,$0,
$0,$0,$36,$40,$0,$0,$46,$66,$66,$66,$66,$66,$66,$40,$0,$0,$0,$0,$55,$0,$6,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,
$63,$20,$63,$20,$20,$25,$63,$20,$20,$36,$66,$66,$66,$63,$56,$66,$66,$66,$53,$20,$20,$20,$35,$20,$26,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$66,$66,$0,$0,$63,$0,$53,$0,$4,$64,$0,$0,$35,$66,$66,$66,$33,$60,$0,$56,$66,$66,$66,$64,$30,$0,$6,$0,$6,$66,$62,$22,$22,$22,$22,$22,$22,$22,$22,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$66,$66,$20,$20,$63,$20,$36,$20,$65,$20,$20,$24,$66,$66,$66,$50,$23,$60,$20,$24,$66,$66,$66,$66,$66,$40,$36,$20,$26,$66,$62,$22,$22,$22,$22,$22,
$22,$22,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$35,$0,$6,$30,$30,$0,$3,$64,$36,$66,$63,$3,$0,$60,$0,$0,$46,$66,$66,$66,$66,$66,$65,$0,$6,$66,$62,$22,
$22,$22,$22,$22,$22,$22,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$26,$20,$20,$63,$20,$20,$66,$30,$26,$64,$20,$56,$30,$60,$20,$20,$26,$33,$56,$66,$66,$66,$63,$20,
$26,$66,$62,$22,$22,$22,$22,$22,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$3,$50,$0,$36,$30,$3,$30,$0,$4,$0,$36,$66,$30,$60,$0,$0,$3,$50,$0,$36,
$66,$64,$30,$0,$6,$66,$62,$22,$22,$22,$22,$22,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$55,$20,$23,$65,$20,$23,$64,$20,$35,$66,$66,$30,$60,$20,$20,
$20,$63,$20,$25,$64,$20,$20,$20,$26,$66,$62,$22,$22,$22,$22,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$5,$63,$0,$4,$65,$56,$46,$3,$66,$66,$66,
$30,$60,$0,$0,$0,$35,$0,$6,$30,$0,$0,$0,$6,$66,$62,$22,$22,$22,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$36,$53,$20,$23,$40,$26,
$23,$66,$66,$66,$30,$60,$20,$20,$20,$26,$20,$36,$20,$20,$20,$20,$26,$66,$62,$22,$22,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$35,
$65,$0,$0,$6,$3,$66,$66,$66,$30,$60,$0,$0,$0,$3,$40,$34,$0,$0,$0,$0,$6,$66,$62,$22,$22,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,
$20,$20,$20,$25,$65,$33,$20,$26,$23,$66,$66,$66,$30,$60,$20,$20,$20,$20,$60,$53,$20,$20,$20,$20,$26,$66,$62,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$66,$66,$0,$0,$0,$0,$0,$4,$66,$66,$66,$66,$3,$66,$66,$66,$30,$60,$0,$0,$0,$0,$53,$50,$0,$0,$0,$0,$6,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$23,$66,$66,$66,$30,$60,$20,$20,$20,$20,$36,$60,$20,$20,$20,$20,$26,$66,$60,$20,$20,$20,$20,$20,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$4,$30,$33,$33,$33,$33,$66,$66,$66,$30,$60,$0,$0,$0,$0,$6,$50,$0,$0,$0,$0,$6,$66,$62,$2,
$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$20,$25,$40,$66,$66,$66,$66,$66,$66,$66,$30,$60,$20,$20,$20,$20,$25,$50,$20,$20,$20,$20,
$26,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$5,$40,$66,$66,$66,$66,$66,$66,$66,$30,$60,$0,$0,$0,$0,$3,$60,
$0,$0,$0,$0,$6,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$20,$25,$40,$66,$66,$66,$66,$66,$66,$66,$30,$60,$20,$20,
$20,$20,$23,$60,$20,$20,$20,$20,$26,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$5,$40,$66,$66,$66,$66,$66,$66,$66,
$30,$60,$0,$0,$0,$0,$0,$60,$0,$0,$0,$0,$6,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$20,$25,$40,$66,$66,$66,
$66,$66,$66,$65,$20,$60,$20,$20,$20,$20,$20,$60,$20,$20,$20,$20,$26,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$5,
$40,$66,$66,$66,$66,$66,$66,$30,$3,$60,$0,$0,$0,$0,$0,$60,$0,$0,$0,$0,$6,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,
$20,$20,$20,$25,$40,$66,$66,$66,$66,$66,$50,$20,$56,$63,$20,$20,$20,$20,$23,$60,$20,$20,$20,$20,$26,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$66,$66,$0,$0,$0,$0,$0,$5,$40,$66,$66,$66,$66,$63,$0,$36,$66,$66,$64,$44,$44,$44,$46,$30,$0,$0,$0,$0,$6,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$20,$25,$40,$66,$66,$66,$63,$20,$36,$66,$66,$66,$66,$66,$66,$66,$63,$20,$20,$20,$20,$20,$26,$66,$60,$20,$20,$20,$20,$20,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$5,$40,$66,$66,$65,$0,$4,$66,$66,$66,$66,$66,$66,$66,$63,$0,$0,$0,$0,$0,$0,$6,$66,$62,$2,
$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$20,$25,$40,$66,$66,$30,$23,$66,$66,$66,$66,$66,$66,$66,$65,$20,$20,$20,$20,$20,$20,$20,
$26,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$5,$40,$66,$40,$3,$56,$66,$66,$66,$66,$66,$66,$66,$30,$0,$0,$0,
$0,$0,$0,$0,$6,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$20,$25,$40,$53,$20,$56,$66,$66,$66,$66,$66,$66,$66,$40,
$20,$20,$20,$20,$20,$20,$20,$20,$26,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$5,$40,$0,$36,$66,$66,$66,$66,$66,
$66,$66,$50,$0,$0,$0,$0,$0,$0,$0,$0,$0,$6,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,$20,$20,$20,$25,$50,$35,$66,$66,
$66,$66,$66,$66,$66,$63,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$26,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$0,$0,$0,$0,$0,$5,
$54,$66,$66,$66,$66,$66,$66,$66,$64,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$6,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$22,$66,$66,$20,$20,
$20,$20,$20,$23,$66,$66,$66,$66,$66,$66,$66,$65,$30,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$26,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$66,$66,$0,$0,$0,$0,$0,$0,$66,$66,$66,$66,$66,$66,$66,$30,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$6,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$66,$66,$20,$20,$20,$20,$20,$20,$36,$66,$66,$66,$66,$66,$50,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$26,$66,$60,$20,$20,$20,$20,$20,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$66,$66,$0,$0,$0,$0,$0,$0,$0,$35,$55,$55,$55,$53,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$6,$66,$62,$2,
$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$66,$66,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$26,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$66,$66,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,
$0,$0,$0,$0,$6,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$66,$66,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$26,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$66,$66,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,
$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$0,$6,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$66,$66,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$26,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$66,$66,$66,$66,$66,$66,$66,$66,
$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$66,$66,$66,$66,
$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$60,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$62,$2,$2,$2,$2,$2,$2,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$66,$60,$20,$20,$20,$20,$20,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$0,
$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,
$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$20,$0,$0,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,
$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$2,$0,$0,$0,$0,$6,$54,$4C,$61,$62,
$65,$6C,$9,$6C,$62,$57,$65,$6C,$63,$6F,$6D,$65,$4,$4C,$65,$66,$74,$3,$B4,$0,$3,$54,$6F,$70,$2,$16,$5,$57,$69,$64,$74,$68,$3,$D8,$0,$6,$48,$65,$69,$67,
$68,$74,$2,$26,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$22,$57,$65,$6C,$63,$6F,$6D,$65,$20,$74,$6F,$20,$25,$73,$20,$43,$6F,$6D,$70,$6F,$6E,$65,$6E,$74,$20,$43,$6F,
$6E,$76,$65,$72,$74,$65,$72,$20,$C,$46,$6F,$6E,$74,$2E,$43,$68,$61,$72,$73,$65,$74,$7,$F,$52,$55,$53,$53,$49,$41,$4E,$5F,$43,$48,$41,$52,$53,$45,$54,$A,$46,
$6F,$6E,$74,$2E,$43,$6F,$6C,$6F,$72,$7,$C,$63,$6C,$57,$69,$6E,$64,$6F,$77,$54,$65,$78,$74,$B,$46,$6F,$6E,$74,$2E,$48,$65,$69,$67,$68,$74,$2,$F0,$9,$46,$6F,
$6E,$74,$2E,$4E,$61,$6D,$65,$6,$5,$41,$72,$69,$61,$6C,$A,$46,$6F,$6E,$74,$2E,$53,$74,$79,$6C,$65,$B,$6,$66,$73,$42,$6F,$6C,$64,$0,$A,$50,$61,$72,$65,$6E,
$74,$46,$6F,$6E,$74,$8,$8,$57,$6F,$72,$64,$57,$72,$61,$70,$9,$0,$0,$6,$54,$4C,$61,$62,$65,$6C,$7,$6C,$62,$57,$54,$65,$78,$74,$4,$4C,$65,$66,$74,$3,$B6,
$0,$3,$54,$6F,$70,$2,$46,$5,$57,$69,$64,$74,$68,$3,$CA,$0,$6,$48,$65,$69,$67,$68,$74,$2,$1C,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$40,$54,$68,$69,$73,$20,
$77,$69,$6C,$6C,$20,$63,$6F,$6E,$76,$65,$72,$74,$20,$79,$6F,$75,$72,$20,$63,$6F,$6D,$70,$6F,$6E,$65,$6E,$74,$27,$73,$20,$74,$6F,$20,$25,$73,$20,$63,$6F,$6D,$70,
$61,$74,$69,$62,$6C,$65,$20,$6F,$6E,$20,$74,$68,$65,$20,$66,$6F,$72,$6D,$2E,$8,$57,$6F,$72,$64,$57,$72,$61,$70,$9,$0,$0,$6,$54,$4C,$61,$62,$65,$6C,$9,$6C,
$62,$57,$61,$72,$6E,$69,$6E,$67,$4,$4C,$65,$66,$74,$3,$B6,$0,$3,$54,$6F,$70,$2,$70,$5,$57,$69,$64,$74,$68,$3,$E8,$0,$6,$48,$65,$69,$67,$68,$74,$2,$2A,
$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$79,$49,$74,$20,$69,$73,$20,$73,$74,$72,$6F,$6E,$67,$6C,$79,$20,$72,$65,$63,$6F,$6D,$65,$6E,$64,$65,$64,$20,$74,$68,$61,$74,
$20,$79,$6F,$75,$20,$6D,$61,$6B,$65,$20,$62,$61,$63,$6B,$75,$70,$20,$6F,$66,$20,$74,$68,$69,$73,$20,$66,$6F,$72,$6D,$2E,$20,$42,$65,$63,$61,$75,$73,$65,$20,$74,
$68,$69,$73,$20,$63,$6F,$6E,$76,$65,$72,$74,$65,$72,$20,$72,$65,$6D,$6F,$76,$65,$20,$61,$6C,$6C,$20,$63,$6F,$6E,$76,$65,$72,$74,$65,$64,$20,$63,$6F,$6D,$70,$6F,
$6E,$65,$6E,$74,$73,$20,$66,$72,$6F,$6D,$2E,$8,$57,$6F,$72,$64,$57,$72,$61,$70,$9,$0,$0,$6,$54,$4C,$61,$62,$65,$6C,$6,$4C,$61,$62,$65,$6C,$31,$4,$4C,$65,
$66,$74,$3,$B6,$0,$3,$54,$6F,$70,$3,$AC,$0,$5,$57,$69,$64,$74,$68,$3,$F0,$0,$6,$48,$65,$69,$67,$68,$74,$2,$E,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$34,
$43,$6C,$69,$63,$6B,$20,$4E,$65,$78,$74,$20,$74,$6F,$20,$63,$6F,$6E,$74,$69,$6E,$75,$65,$2C,$20,$6F,$72,$20,$43,$61,$6E,$63,$61,$6C,$20,$74,$6F,$20,$65,$78,$69,
$74,$20,$63,$6F,$6E,$76,$65,$72,$74,$65,$72,$2E,$0,$0,$0,$5,$54,$50,$61,$67,$65,$0,$4,$4C,$65,$66,$74,$2,$0,$3,$54,$6F,$70,$2,$0,$7,$43,$61,$70,$74,
$69,$6F,$6E,$6,$5,$43,$6F,$6D,$70,$73,$0,$6,$54,$42,$65,$76,$65,$6C,$6,$42,$65,$76,$65,$6C,$32,$4,$4C,$65,$66,$74,$2,$0,$3,$54,$6F,$70,$2,$0,$5,$57,
$69,$64,$74,$68,$3,$DA,$1,$6,$48,$65,$69,$67,$68,$74,$2,$3B,$5,$41,$6C,$69,$67,$6E,$7,$5,$61,$6C,$54,$6F,$70,$5,$53,$68,$61,$70,$65,$7,$C,$62,$73,$42,
$6F,$74,$74,$6F,$6D,$4C,$69,$6E,$65,$0,$0,$6,$54,$4C,$61,$62,$65,$6C,$6,$4C,$61,$62,$65,$6C,$34,$4,$4C,$65,$66,$74,$2,$C,$3,$54,$6F,$70,$2,$14,$5,$57,
$69,$64,$74,$68,$3,$96,$0,$6,$48,$65,$69,$67,$68,$74,$2,$13,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$11,$53,$65,$6C,$65,$63,$74,$20,$43,$6F,$6D,$70,$6F,$6E,$65,
$6E,$74,$73,$C,$46,$6F,$6E,$74,$2E,$43,$68,$61,$72,$73,$65,$74,$7,$F,$52,$55,$53,$53,$49,$41,$4E,$5F,$43,$48,$41,$52,$53,$45,$54,$A,$46,$6F,$6E,$74,$2E,$43,
$6F,$6C,$6F,$72,$7,$C,$63,$6C,$57,$69,$6E,$64,$6F,$77,$54,$65,$78,$74,$B,$46,$6F,$6E,$74,$2E,$48,$65,$69,$67,$68,$74,$2,$F0,$9,$46,$6F,$6E,$74,$2E,$4E,$61,
$6D,$65,$6,$5,$41,$72,$69,$61,$6C,$A,$46,$6F,$6E,$74,$2E,$53,$74,$79,$6C,$65,$B,$6,$66,$73,$42,$6F,$6C,$64,$0,$A,$50,$61,$72,$65,$6E,$74,$46,$6F,$6E,$74,
$8,$0,$0,$6,$54,$50,$61,$6E,$65,$6C,$6,$50,$61,$6E,$65,$6C,$31,$4,$4C,$65,$66,$74,$2,$0,$3,$54,$6F,$70,$2,$3B,$5,$57,$69,$64,$74,$68,$3,$DA,$1,$6,
$48,$65,$69,$67,$68,$74,$3,$FC,$0,$5,$41,$6C,$69,$67,$6E,$7,$8,$61,$6C,$43,$6C,$69,$65,$6E,$74,$A,$42,$65,$76,$65,$6C,$4F,$75,$74,$65,$72,$7,$6,$62,$76,
$4E,$6F,$6E,$65,$8,$54,$61,$62,$4F,$72,$64,$65,$72,$2,$0,$0,$6,$54,$4C,$61,$62,$65,$6C,$6,$4C,$61,$62,$65,$6C,$35,$4,$4C,$65,$66,$74,$2,$6,$3,$54,$6F,
$70,$2,$8,$5,$57,$69,$64,$74,$68,$2,$38,$6,$48,$65,$69,$67,$68,$74,$2,$E,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$B,$43,$6F,$6E,$76,$65,$72,$74,$20,$54,$6F,
$3A,$0,$0,$9,$54,$47,$72,$6F,$75,$70,$42,$6F,$78,$9,$47,$72,$6F,$75,$70,$42,$6F,$78,$31,$4,$4C,$65,$66,$74,$3,$BE,$0,$3,$54,$6F,$70,$2,$14,$5,$57,$69,
$64,$74,$68,$3,$15,$1,$6,$48,$65,$69,$67,$68,$74,$3,$DF,$0,$7,$41,$6E,$63,$68,$6F,$72,$73,$B,$6,$61,$6B,$4C,$65,$66,$74,$5,$61,$6B,$54,$6F,$70,$8,$61,
$6B,$42,$6F,$74,$74,$6F,$6D,$0,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$D,$43,$6F,$6E,$76,$65,$72,$74,$20,$46,$72,$6F,$6D,$3A,$8,$54,$61,$62,$4F,$72,$64,$65,$72,
$2,$0,$0,$8,$54,$4C,$69,$73,$74,$42,$6F,$78,$A,$6C,$62,$4F,$6C,$64,$43,$6F,$6D,$70,$73,$4,$4C,$65,$66,$74,$2,$C,$3,$54,$6F,$70,$2,$16,$5,$57,$69,$64,
$74,$68,$3,$A9,$0,$6,$48,$65,$69,$67,$68,$74,$3,$BD,$0,$7,$41,$6E,$63,$68,$6F,$72,$73,$B,$6,$61,$6B,$4C,$65,$66,$74,$5,$61,$6B,$54,$6F,$70,$8,$61,$6B,
$42,$6F,$74,$74,$6F,$6D,$0,$A,$49,$74,$65,$6D,$48,$65,$69,$67,$68,$74,$2,$E,$8,$54,$61,$62,$4F,$72,$64,$65,$72,$2,$0,$0,$0,$7,$54,$42,$75,$74,$74,$6F,
$6E,$9,$62,$74,$6E,$41,$64,$64,$4F,$6C,$64,$4,$4C,$65,$66,$74,$3,$BE,$0,$3,$54,$6F,$70,$2,$16,$5,$57,$69,$64,$74,$68,$2,$4B,$6,$48,$65,$69,$67,$68,$74,
$2,$19,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$6,$41,$64,$64,$2E,$2E,$2E,$8,$54,$61,$62,$4F,$72,$64,$65,$72,$2,$1,$7,$4F,$6E,$43,$6C,$69,$63,$6B,$7,$E,$62,
$74,$6E,$41,$64,$64,$4F,$6C,$64,$43,$6C,$69,$63,$6B,$0,$0,$0,$D,$54,$43,$68,$65,$63,$6B,$4C,$69,$73,$74,$42,$6F,$78,$A,$6C,$62,$4E,$65,$77,$43,$6F,$6D,$70,
$73,$4,$4C,$65,$66,$74,$2,$6,$3,$54,$6F,$70,$2,$1A,$5,$57,$69,$64,$74,$68,$3,$AF,$0,$6,$48,$65,$69,$67,$68,$74,$3,$D9,$0,$7,$41,$6E,$63,$68,$6F,$72,
$73,$B,$6,$61,$6B,$4C,$65,$66,$74,$5,$61,$6B,$54,$6F,$70,$8,$61,$6B,$42,$6F,$74,$74,$6F,$6D,$0,$A,$49,$74,$65,$6D,$48,$65,$69,$67,$68,$74,$2,$E,$8,$54,
$61,$62,$4F,$72,$64,$65,$72,$2,$1,$7,$4F,$6E,$43,$6C,$69,$63,$6B,$7,$F,$6C,$62,$4E,$65,$77,$43,$6F,$6D,$70,$73,$43,$6C,$69,$63,$6B,$0,$0,$0,$0,$5,$54,
$50,$61,$67,$65,$0,$4,$4C,$65,$66,$74,$2,$0,$3,$54,$6F,$70,$2,$0,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$7,$43,$6F,$6E,$76,$65,$72,$74,$0,$6,$54,$42,$65,
$76,$65,$6C,$6,$42,$65,$76,$65,$6C,$33,$4,$4C,$65,$66,$74,$2,$0,$3,$54,$6F,$70,$2,$0,$5,$57,$69,$64,$74,$68,$3,$DA,$1,$6,$48,$65,$69,$67,$68,$74,$2,
$3B,$5,$41,$6C,$69,$67,$6E,$7,$5,$61,$6C,$54,$6F,$70,$5,$53,$68,$61,$70,$65,$7,$C,$62,$73,$42,$6F,$74,$74,$6F,$6D,$4C,$69,$6E,$65,$0,$0,$6,$54,$4C,$61,
$62,$65,$6C,$6,$4C,$61,$62,$65,$6C,$37,$4,$4C,$65,$66,$74,$2,$C,$3,$54,$6F,$70,$2,$14,$5,$57,$69,$64,$74,$68,$2,$55,$6,$48,$65,$69,$67,$68,$74,$2,$13,
$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$A,$43,$6F,$6E,$76,$65,$72,$74,$69,$6F,$6E,$C,$46,$6F,$6E,$74,$2E,$43,$68,$61,$72,$73,$65,$74,$7,$F,$52,$55,$53,$53,$49,
$41,$4E,$5F,$43,$48,$41,$52,$53,$45,$54,$A,$46,$6F,$6E,$74,$2E,$43,$6F,$6C,$6F,$72,$7,$C,$63,$6C,$57,$69,$6E,$64,$6F,$77,$54,$65,$78,$74,$B,$46,$6F,$6E,$74,
$2E,$48,$65,$69,$67,$68,$74,$2,$F0,$9,$46,$6F,$6E,$74,$2E,$4E,$61,$6D,$65,$6,$5,$41,$72,$69,$61,$6C,$A,$46,$6F,$6E,$74,$2E,$53,$74,$79,$6C,$65,$B,$6,$66,
$73,$42,$6F,$6C,$64,$0,$A,$50,$61,$72,$65,$6E,$74,$46,$6F,$6E,$74,$8,$0,$0,$6,$54,$50,$61,$6E,$65,$6C,$6,$50,$61,$6E,$65,$6C,$32,$4,$4C,$65,$66,$74,$2,
$0,$3,$54,$6F,$70,$2,$3B,$5,$57,$69,$64,$74,$68,$3,$DA,$1,$6,$48,$65,$69,$67,$68,$74,$3,$FC,$0,$5,$41,$6C,$69,$67,$6E,$7,$8,$61,$6C,$43,$6C,$69,$65,
$6E,$74,$A,$42,$65,$76,$65,$6C,$4F,$75,$74,$65,$72,$7,$6,$62,$76,$4E,$6F,$6E,$65,$8,$54,$61,$62,$4F,$72,$64,$65,$72,$2,$0,$0,$6,$54,$4C,$61,$62,$65,$6C,
$6,$4C,$61,$62,$65,$6C,$36,$4,$4C,$65,$66,$74,$2,$6,$3,$54,$6F,$70,$2,$A,$5,$57,$69,$64,$74,$68,$2,$48,$6,$48,$65,$69,$67,$68,$74,$2,$E,$7,$43,$61,
$70,$74,$69,$6F,$6E,$6,$F,$43,$6F,$6E,$76,$65,$72,$74,$69,$6F,$6E,$20,$6C,$6F,$67,$3A,$0,$0,$5,$54,$4D,$65,$6D,$6F,$7,$4C,$6F,$67,$4D,$65,$6D,$6F,$4,$4C,
$65,$66,$74,$2,$6,$3,$54,$6F,$70,$2,$1C,$5,$57,$69,$64,$74,$68,$3,$CB,$1,$6,$48,$65,$69,$67,$68,$74,$3,$C3,$0,$7,$41,$6E,$63,$68,$6F,$72,$73,$B,$6,
$61,$6B,$4C,$65,$66,$74,$5,$61,$6B,$54,$6F,$70,$7,$61,$6B,$52,$69,$67,$68,$74,$8,$61,$6B,$42,$6F,$74,$74,$6F,$6D,$0,$8,$52,$65,$61,$64,$4F,$6E,$6C,$79,$9,
$A,$53,$63,$72,$6F,$6C,$6C,$42,$61,$72,$73,$7,$A,$73,$73,$56,$65,$72,$74,$69,$63,$61,$6C,$8,$54,$61,$62,$4F,$72,$64,$65,$72,$2,$0,$8,$57,$6F,$72,$64,$57,
$72,$61,$70,$8,$0,$0,$C,$54,$50,$72,$6F,$67,$72,$65,$73,$73,$42,$61,$72,$5,$43,$50,$72,$6F,$67,$4,$4C,$65,$66,$74,$2,$6,$3,$54,$6F,$70,$3,$E6,$0,$5,
$57,$69,$64,$74,$68,$3,$CB,$1,$6,$48,$65,$69,$67,$68,$74,$2,$10,$7,$41,$6E,$63,$68,$6F,$72,$73,$B,$6,$61,$6B,$4C,$65,$66,$74,$7,$61,$6B,$52,$69,$67,$68,
$74,$8,$61,$6B,$42,$6F,$74,$74,$6F,$6D,$0,$3,$4D,$69,$6E,$2,$0,$3,$4D,$61,$78,$2,$64,$8,$54,$61,$62,$4F,$72,$64,$65,$72,$2,$1,$0,$0,$0,$0,$0,$7,
$54,$42,$69,$74,$42,$74,$6E,$9,$62,$74,$6E,$43,$61,$6E,$63,$65,$6C,$4,$4C,$65,$66,$74,$3,$82,$1,$3,$54,$6F,$70,$3,$4D,$1,$5,$57,$69,$64,$74,$68,$2,$4B,
$6,$48,$65,$69,$67,$68,$74,$2,$19,$7,$41,$6E,$63,$68,$6F,$72,$73,$B,$7,$61,$6B,$52,$69,$67,$68,$74,$8,$61,$6B,$42,$6F,$74,$74,$6F,$6D,$0,$8,$54,$61,$62,
$4F,$72,$64,$65,$72,$2,$1,$4,$4B,$69,$6E,$64,$7,$8,$62,$6B,$43,$61,$6E,$63,$65,$6C,$0,$0,$7,$54,$42,$69,$74,$42,$74,$6E,$7,$62,$74,$6E,$4E,$65,$78,$74,
$4,$4C,$65,$66,$74,$3,$2E,$1,$3,$54,$6F,$70,$3,$4D,$1,$5,$57,$69,$64,$74,$68,$2,$4B,$6,$48,$65,$69,$67,$68,$74,$2,$19,$7,$41,$6E,$63,$68,$6F,$72,$73,
$B,$7,$61,$6B,$52,$69,$67,$68,$74,$8,$61,$6B,$42,$6F,$74,$74,$6F,$6D,$0,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$7,$26,$4E,$65,$78,$74,$20,$3E,$8,$54,$61,$62,
$4F,$72,$64,$65,$72,$2,$2,$7,$4F,$6E,$43,$6C,$69,$63,$6B,$7,$C,$62,$74,$6E,$4E,$65,$78,$74,$43,$6C,$69,$63,$6B,$0,$0,$7,$54,$42,$69,$74,$42,$74,$6E,$7,
$62,$74,$6E,$42,$61,$63,$6B,$4,$4C,$65,$66,$74,$3,$E2,$0,$3,$54,$6F,$70,$3,$4D,$1,$5,$57,$69,$64,$74,$68,$2,$4B,$6,$48,$65,$69,$67,$68,$74,$2,$19,$7,
$41,$6E,$63,$68,$6F,$72,$73,$B,$7,$61,$6B,$52,$69,$67,$68,$74,$8,$61,$6B,$42,$6F,$74,$74,$6F,$6D,$0,$7,$43,$61,$70,$74,$69,$6F,$6E,$6,$7,$3C,$20,$26,$42,
$61,$63,$6B,$8,$54,$61,$62,$4F,$72,$64,$65,$72,$2,$3,$7,$4F,$6E,$43,$6C,$69,$63,$6B,$7,$C,$62,$74,$6E,$42,$61,$63,$6B,$43,$6C,$69,$63,$6B,$0,$0,$7,$54,
$42,$69,$74,$42,$74,$6E,$8,$62,$74,$6E,$43,$6C,$6F,$73,$65,$4,$4C,$65,$66,$74,$3,$82,$1,$3,$54,$6F,$70,$3,$4D,$1,$5,$57,$69,$64,$74,$68,$2,$4B,$6,$48,
$65,$69,$67,$68,$74,$2,$19,$7,$41,$6E,$63,$68,$6F,$72,$73,$B,$7,$61,$6B,$52,$69,$67,$68,$74,$8,$61,$6B,$42,$6F,$74,$74,$6F,$6D,$0,$7,$43,$61,$70,$74,$69,
$6F,$6E,$6,$5,$43,$6C,$6F,$73,$65,$8,$54,$61,$62,$4F,$72,$64,$65,$72,$2,$4,$7,$56,$69,$73,$69,$62,$6C,$65,$8,$4,$4B,$69,$6E,$64,$7,$4,$62,$6B,$4F,$4B,
$0,$0,$7,$54,$42,$69,$74,$42,$74,$6E,$8,$62,$74,$6E,$53,$74,$61,$72,$74,$4,$4C,$65,$66,$74,$3,$2E,$1,$3,$54,$6F,$70,$3,$4D,$1,$5,$57,$69,$64,$74,$68,
$2,$4B,$6,$48,$65,$69,$67,$68,$74,$2,$19,$7,$41,$6E,$63,$68,$6F,$72,$73,$B,$7,$61,$6B,$52,$69,$67,$68,$74,$8,$61,$6B,$42,$6F,$74,$74,$6F,$6D,$0,$7,$43,
$61,$70,$74,$69,$6F,$6E,$6,$5,$53,$74,$61,$72,$74,$8,$54,$61,$62,$4F,$72,$64,$65,$72,$2,$5,$7,$56,$69,$73,$69,$62,$6C,$65,$8,$7,$4F,$6E,$43,$6C,$69,$63,
$6B,$7,$D,$62,$74,$6E,$53,$74,$61,$72,$74,$43,$6C,$69,$63,$6B,$0,$0,$0,$0);

constructor TfrmConvertForm.CreateForm;
var
  F: TForm;
  M: TMemoryStream;
begin
  inherited CreateNew(Application);

  { Load from  }
  M := TMemoryStream.Create;
  try
    M.Write(ConvertFormRes, SizeOf(ConvertFormRes));
    M.Seek(0, soFromBeginning);

    M.ReadResHeader;

    M.ReadComponent(Self);
  finally
    M.Free;
  end;
end;

procedure TfrmConvertForm.FormCreate(Sender: TObject);
var
  i: integer;
begin
  { Initialization }
  Pages.PageIndex := 0;

  FConvertClass := TTeCustomConverter;
  FConvertName := 'Kernel';
end;

destructor TfrmConvertForm.Destroy;
begin
  FConverter.Free;
  inherited;
end;

procedure TfrmConvertForm.InitConverter;
begin
  { Set control's property }
  lbWelcome.Caption := Format(lbWelcome.Caption, [FConvertName]);
  lbWText.Caption := Format(lbWText.Caption, [FConvertName]);

  FConverter := FConvertClass.Create(Self);

  BuildNewCompsList;
end;

procedure TfrmConvertForm.Convert;
begin
  Pages.PageIndex := Pages.PageIndex + 1;
  btnClose.Visible := true;
  btnClose.Enabled := false;
  btnNext.Visible := false;
  btnBack.Visible := false;
  btnStart.Visible := false;
  Application.ProcessMessages;
  try
    { Convertion }
    FConverter.OnProgress := DoConvert;
    FConverter.Log := LogMemo.Lines;

    FConverter.ConvertForm(FConvertForm);
  finally
    btnCancel.Visible := false;
    btnClose.Enabled := true;
  end;
end;

procedure TfrmConvertForm.BuildNewCompsList;
var
  i: integer;
  AObjectClass: TComponentClass;
  AObject: TComponent;
begin
  lbNewComps.Items.Clear;
  if FConverter = nil then Exit;

  for i := 0 to Integer(ckLast) - 1 do
  begin
    AObjectClass := FConverter.GetClass(TTeClassKind(i));
    if AObjectClass = nil then Continue;

    AObject := AObjectClass.Create(nil);
    try
      if AObject <> nil then
        lbNewComps.Items.AddObject(AObject.ClassName, Pointer(i));

      lbNewComps.Checked[i] := true;
    finally
      AObject.Free;
    end;
  end;
end;

procedure TfrmConvertForm.BuildOldCompsList;
var
  Kind: TTeClassKind;
  ClassList: string;
  i: integer;
begin
  { New Comps clicked }
  lbOldComps.Items.Clear;

  if (lbNewComps.ItemIndex < 0) or (lbNewComps.ItemIndex > lbNewComps.Items.Count - 1) then Exit;
  if FConverter = nil then Exit;

  Kind := TTeClassKind(lbNewComps.Items.Objects[lbNewComps.ItemIndex]);

  ClassList := FConverter.Rules[Kind];

  for i := 0 to GetClassCount(ClassList) - 1 do
    lbOldComps.Items.Add(GetClassName(ClassList, i));
end;

procedure TfrmConvertForm.PagesPageChanged(Sender: TObject);
begin
  case Pages.PageIndex of
    0: begin { Welcome }
      btnBack.Visible := false;
      btnNext.Visible := true;
      btnStart.Visible := false;
    end;
    1: begin { Comps }
      btnBack.Visible := true;
      btnNext.Visible := true;

      btnStart.Visible := true;
    end;
    2: begin { Convert }
      btnBack.Visible := true;
      btnNext.Visible := false;
    end;
  else
    btnBack.Visible := true;
    btnNext.Visible := true;
  end;
end;

procedure TfrmConvertForm.btnNextClick(Sender: TObject);
begin
  Pages.PageIndex := Pages.PageIndex + 1;
end;

procedure TfrmConvertForm.btnBackClick(Sender: TObject);
begin
  Pages.PageIndex := Pages.PageIndex - 1;
end;

procedure TfrmConvertForm.SetConvertClass(const Value: TTeConverterClass);
begin
  FConvertClass := Value;
end;

procedure TfrmConvertForm.SetConvertName(const Value: string);
begin
  FConvertName := Value;
end;

procedure TfrmConvertForm.btnStartClick(Sender: TObject);
begin
  Convert;
end;

procedure TfrmConvertForm.DoConvert(Sender: TObject; Max, Pos: integer);
begin
  CProg.Max := Max;
  CProg.Position := Pos;
end;

procedure TfrmConvertForm.SetConvertForm(const Value: TCustomForm);
begin
  FConvertForm := Value;
end;

procedure TfrmConvertForm.lbNewCompsClick(Sender: TObject);
begin
  BuildOldCompsList;
end;

procedure TfrmConvertForm.btnAddOldClick(Sender: TObject);
var
  NewClass: string;
  Kind: TTeClassKind;
  ClassList: string;
begin
  if FConverter = nil then Exit;

  frmAddClass := TfrmAddClass.CreateNew(Self);
  if frmAddClass.ShowModal = mrOk then
    if frmAddClass.cbClassList.ItemIndex >= 0 then
    begin
      NewClass := frmAddClass.cbClassList.Items[frmAddClass.cbClassList.ItemIndex];
      Kind := TTeClassKind(lbNewComps.Items.Objects[lbNewComps.ItemIndex]);

      ClassList := FConverter.Rules[Kind];
      AddClassName(ClassList, NewClass);
      FConverter.Rules[Kind] := ClassList;

      BuildOldCompsList;
    end;
  frmAddClass.Free;
end;


{ MenuDesigner ================================================================}


const MenuDesignerRes: array [0..2654] of byte = (
$FF,
$A, $0, $54, $46, $52, $4D, $4D, $45, $4E, $55, $44, $45, $53, $49, $47, $4E, $45, $52, $46, $4F,
$52, $4D, $0, $30, $10, $40, $A, $0, $0, $54, $50, $46, $30, $14, $54, $66, $72, $6D, $4D, $65,
$6E, $75, $44, $65, $73, $69, $67, $6E, $65, $72, $46, $6F, $72, $6D, $13, $66, $72, $6D, $4D, $65,
$6E, $75, $44, $65, $73, $69, $67, $6E, $65, $72, $46, $6F, $72, $6D, $4, $4C, $65, $66, $74, $3,
$B0, $1, $3, $54, $6F, $70, $3, $B3, $0, $5, $57, $69, $64, $74, $68, $3, $34, $1, $6, $48,
$65, $69, $67, $68, $74, $3, $8A, $1, $B, $42, $6F, $72, $64, $65, $72, $53, $74, $79, $6C, $65,
$7, $D, $62, $73, $53, $69, $7A, $65, $54, $6F, $6F, $6C, $57, $69, $6E, $7, $43, $61, $70, $74,
$69, $6F, $6E, $6, $B, $4B, $53, $20, $44, $65, $73, $69, $67, $6E, $65, $72, $5, $43, $6F, $6C,
$6F, $72, $7, $9, $63, $6C, $42, $74, $6E, $46, $61, $63, $65, $C, $46, $6F, $6E, $74, $2E, $43,
$68, $61, $72, $73, $65, $74, $7, $F, $44, $45, $46, $41, $55, $4C, $54, $5F, $43, $48, $41, $52,
$53, $45, $54, $A, $46, $6F, $6E, $74, $2E, $43, $6F, $6C, $6F, $72, $7, $C, $63, $6C, $57, $69,
$6E, $64, $6F, $77, $54, $65, $78, $74, $B, $46, $6F, $6E, $74, $2E, $48, $65, $69, $67, $68, $74,
$2, $F5, $9, $46, $6F, $6E, $74, $2E, $4E, $61, $6D, $65, $6, $D, $4D, $53, $20, $53, $61, $6E,
$73, $20, $53, $65, $72, $69, $66, $A, $46, $6F, $6E, $74, $2E, $53, $74, $79, $6C, $65, $B, $0,
$E, $4F, $6C, $64, $43, $72, $65, $61, $74, $65, $4F, $72, $64, $65, $72, $8, $8, $50, $6F, $73,
$69, $74, $69, $6F, $6E, $7, $E, $70, $6F, $53, $63, $72, $65, $65, $6E, $43, $65, $6E, $74, $65,
$72, $7, $4F, $6E, $43, $6C, $6F, $73, $65, $7, $9, $46, $6F, $72, $6D, $43, $6C, $6F, $73, $65,
$8, $4F, $6E, $43, $72, $65, $61, $74, $65, $7, $A, $46, $6F, $72, $6D, $43, $72, $65, $61, $74,
$65, $D, $50, $69, $78, $65, $6C, $73, $50, $65, $72, $49, $6E, $63, $68, $2, $60, $A, $54, $65,
$78, $74, $48, $65, $69, $67, $68, $74, $2, $D, $0, $C, $54, $50, $61, $67, $65, $43, $6F, $6E, 
$74, $72, $6F, $6C, $5, $50, $61, $67, $65, $73, $4, $4C, $65, $66, $74, $2, $0, $3, $54, $6F, 
$70, $2, $0, $5, $57, $69, $64, $74, $68, $3, $2C, $1, $6, $48, $65, $69, $67, $68, $74, $3,
$6F, $1, $A, $41, $63, $74, $69, $76, $65, $50, $61, $67, $65, $7, $9, $54, $61, $62, $53, $68, 
$65, $65, $74, $31, $5, $41, $6C, $69, $67, $6E, $7, $8, $61, $6C, $43, $6C, $69, $65, $6E, $74, 
$8, $54, $61, $62, $4F, $72, $64, $65, $72, $2, $0, $8, $4F, $6E, $43, $68, $61, $6E, $67, $65, 
$7, $B, $50, $61, $67, $65, $73, $43, $68, $61, $6E, $67, $65, $0, $9, $54, $54, $61, $62, $53, 
$68, $65, $65, $74, $9, $54, $61, $62, $53, $68, $65, $65, $74, $31, $7, $43, $61, $70, $74, $69, 
$6F, $6E, $6, $5, $49, $74, $65, $6D, $73, $0, $6, $54, $50, $61, $6E, $65, $6C, $6, $50, $61, 
$6E, $65, $6C, $32, $4, $4C, $65, $66, $74, $2, $59, $3, $54, $6F, $70, $2, $0, $5, $57, $69, 
$64, $74, $68, $3, $CB, $0, $6, $48, $65, $69, $67, $68, $74, $3, $53, $1, $5, $41, $6C, $69, 
$67, $6E, $7, $8, $61, $6C, $43, $6C, $69, $65, $6E, $74, $A, $42, $65, $76, $65, $6C, $4F, $75, 
$74, $65, $72, $7, $6, $62, $76, $4E, $6F, $6E, $65, $B, $42, $6F, $72, $64, $65, $72, $57, $69, 
$64, $74, $68, $2, $3, $8, $54, $61, $62, $4F, $72, $64, $65, $72, $2, $0, $0, $9, $54, $54, 
$72, $65, $65, $56, $69, $65, $77, $9, $54, $72, $65, $65, $49, $74, $65, $6D, $73, $4, $4C, $65, 
$66, $74, $2, $3, $3, $54, $6F, $70, $2, $1C, $5, $57, $69, $64, $74, $68, $3, $C5, $0, $6, 
$48, $65, $69, $67, $68, $74, $3, $34, $1, $5, $41, $6C, $69, $67, $6E, $7, $8, $61, $6C, $43, 
$6C, $69, $65, $6E, $74, $5, $43, $6F, $6C, $6F, $72, $7, $9, $63, $6C, $42, $74, $6E, $46, $61, 
$63, $65, $8, $48, $6F, $74, $54, $72, $61, $63, $6B, $9, $6, $49, $6E, $64, $65, $6E, $74, $2, 
$13, $8, $54, $61, $62, $4F, $72, $64, $65, $72, $2, $0, $7, $4F, $6E, $43, $6C, $69, $63, $6B,
$7, $E, $54, $72, $65, $65, $49, $74, $65, $6D, $73, $43, $6C, $69, $63, $6B, $A, $4F, $6E, $44, 
$62, $6C, $43, $6C, $69, $63, $6B, $7, $11, $54, $72, $65, $65, $49, $74, $65, $6D, $73, $44, $62, 
$6C, $43, $6C, $69, $63, $6B, $8, $4F, $6E, $45, $64, $69, $74, $65, $64, $7, $F, $54, $72, $65,
$65, $49, $74, $65, $6D, $73, $45, $64, $69, $74, $65, $64, $0, $0, $6, $54, $50, $61, $6E, $65, 
$6C, $9, $49, $74, $65, $6D, $73, $4E, $61, $6D, $65, $4, $4C, $65, $66, $74, $2, $3, $3, $54, 
$6F, $70, $2, $3, $5, $57, $69, $64, $74, $68, $3, $C5, $0, $6, $48, $65, $69, $67, $68, $74,
$2, $14, $5, $41, $6C, $69, $67, $6E, $7, $5, $61, $6C, $54, $6F, $70, $9, $41, $6C, $69, $67, 
$6E, $6D, $65, $6E, $74, $7, $D, $74, $61, $4C, $65, $66, $74, $4A, $75, $73, $74, $69, $66, $79, 
$A, $42, $65, $76, $65, $6C, $4F, $75, $74, $65, $72, $7, $9, $62, $76, $4C, $6F, $77, $65, $72, 
$65, $64, $7, $43, $61, $70, $74, $69, $6F, $6E, $6, $6, $20, $49, $74, $65, $6D, $73, $5, $43, 
$6F, $6C, $6F, $72, $7, $B, $63, $6C, $42, $74, $6E, $53, $68, $61, $64, $6F, $77, $C, $46, $6F, 
$6E, $74, $2E, $43, $68, $61, $72, $73, $65, $74, $7, $C, $41, $4E, $53, $49, $5F, $43, $48, $41, 
$52, $53, $45, $54, $A, $46, $6F, $6E, $74, $2E, $43, $6F, $6C, $6F, $72, $7, $7, $63, $6C, $57, 
$68, $69, $74, $65, $B, $46, $6F, $6E, $74, $2E, $48, $65, $69, $67, $68, $74, $2, $F3, $9, $46, 
$6F, $6E, $74, $2E, $4E, $61, $6D, $65, $6, $6, $54, $61, $68, $6F, $6D, $61, $A, $46, $6F, $6E, 
$74, $2E, $53, $74, $79, $6C, $65, $B, $6, $66, $73, $42, $6F, $6C, $64, $0, $A, $50, $61, $72, 
$65, $6E, $74, $46, $6F, $6E, $74, $8, $8, $54, $61, $62, $4F, $72, $64, $65, $72, $2, $1, $0, 
$0, $6, $54, $50, $61, $6E, $65, $6C, $6, $50, $61, $6E, $65, $6C, $34, $4, $4C, $65, $66, $74, 
$2, $3, $3, $54, $6F, $70, $2, $17, $5, $57, $69, $64, $74, $68, $3, $C5, $0, $6, $48, $65, 
$69, $67, $68, $74, $2, $5, $5, $41, $6C, $69, $67, $6E, $7, $5, $61, $6C, $54, $6F, $70, $A, 
$42, $65, $76, $65, $6C, $4F, $75, $74, $65, $72, $7, $6, $62, $76, $4E, $6F, $6E, $65, $8, $54, 
$61, $62, $4F, $72, $64, $65, $72, $2, $2, $0, $0, $0, $6, $54, $50, $61, $6E, $65, $6C, $6, 
$50, $61, $6E, $65, $6C, $31, $4, $4C, $65, $66, $74, $2, $0, $3, $54, $6F, $70, $2, $0, $5, 
$57, $69, $64, $74, $68, $2, $59, $6, $48, $65, $69, $67, $68, $74, $3, $53, $1, $5, $41, $6C, 
$69, $67, $6E, $7, $6, $61, $6C, $4C, $65, $66, $74, $A, $42, $65, $76, $65, $6C, $4F, $75, $74, 
$65, $72, $7, $6, $62, $76, $4E, $6F, $6E, $65, $B, $42, $6F, $72, $64, $65, $72, $57, $69, $64,
$74, $68, $2, $3, $8, $54, $61, $62, $4F, $72, $64, $65, $72, $2, $1, $0, $6, $54, $50, $61, 
$6E, $65, $6C, $6, $50, $61, $6E, $65, $6C, $35, $4, $4C, $65, $66, $74, $2, $3, $3, $54, $6F, 
$70, $2, $3, $5, $57, $69, $64, $74, $68, $2, $53, $6, $48, $65, $69, $67, $68, $74, $3, $4D,
$1, $5, $41, $6C, $69, $67, $6E, $7, $8, $61, $6C, $43, $6C, $69, $65, $6E, $74, $A, $42, $65, 
$76, $65, $6C, $4F, $75, $74, $65, $72, $7, $9, $62, $76, $4C, $6F, $77, $65, $72, $65, $64, $8, 
$54, $61, $62, $4F, $72, $64, $65, $72, $2, $0, $0, $6, $54, $42, $65, $76, $65, $6C, $6, $42, 
$65, $76, $65, $6C, $31, $4, $4C, $65, $66, $74, $2, $6, $3, $54, $6F, $70, $2, $5E, $5, $57,
$69, $64, $74, $68, $2, $47, $6, $48, $65, $69, $67, $68, $74, $2, $9, $5, $53, $68, $61, $70, 
$65, $7, $9, $62, $73, $54, $6F, $70, $4C, $69, $6E, $65, $0, $0, $7, $54, $42, $75, $74, $74, 
$6F, $6E, $9, $62, $74, $6E, $49, $6E, $73, $65, $72, $74, $4, $4C, $65, $66, $74, $2, $6, $3, 
$54, $6F, $70, $2, $6, $5, $57, $69, $64, $74, $68, $2, $47, $6, $48, $65, $69, $67, $68, $74, 
$2, $1B, $7, $43, $61, $70, $74, $69, $6F, $6E, $6, $8, $4E, $65, $77, $20, $49, $74, $65, $6D, 
$8, $54, $61, $62, $4F, $72, $64, $65, $72, $2, $0, $7, $4F, $6E, $43, $6C, $69, $63, $6B, $7, 
$E, $62, $74, $6E, $49, $6E, $73, $65, $72, $74, $43, $6C, $69, $63, $6B, $0, $0, $7, $54, $42, 
$75, $74, $74, $6F, $6E, $A, $62, $74, $6E, $53, $75, $62, $4D, $65, $6E, $75, $4, $4C, $65, $66, 
$74, $2, $6, $3, $54, $6F, $70, $2, $24, $5, $57, $69, $64, $74, $68, $2, $47, $6, $48, $65, 
$69, $67, $68, $74, $2, $19, $7, $43, $61, $70, $74, $69, $6F, $6E, $6, $8, $53, $75, $62, $20, 
$49, $74, $65, $6D, $8, $54, $61, $62, $4F, $72, $64, $65, $72, $2, $1, $7, $4F, $6E, $43, $6C, 
$69, $63, $6B, $7, $F, $62, $74, $6E, $53, $75, $62, $4D, $65, $6E, $75, $43, $6C, $69, $63, $6B, 
$0, $0, $7, $54, $42, $75, $74, $74, $6F, $6E, $9, $62, $74, $6E, $44, $65, $6C, $65, $74, $65, 
$4, $4C, $65, $66, $74, $2, $6, $3, $54, $6F, $70, $2, $40, $5, $57, $69, $64, $74, $68, $2, 
$47, $6, $48, $65, $69, $67, $68, $74, $2, $19, $7, $43, $61, $70, $74, $69, $6F, $6E, $6, $6, 
$44, $65, $6C, $65, $74, $65, $8, $54, $61, $62, $4F, $72, $64, $65, $72, $2, $2, $7, $4F, $6E,
$43, $6C, $69, $63, $6B, $7, $E, $62, $74, $6E, $44, $65, $6C, $65, $74, $65, $43, $6C, $69, $63, 
$6B, $0, $0, $7, $54, $42, $69, $74, $42, $74, $6E, $5, $62, $74, $6E, $55, $70, $4, $4C, $65, 
$66, $74, $2, $6, $3, $54, $6F, $70, $2, $66, $5, $57, $69, $64, $74, $68, $2, $47, $6, $48,
$65, $69, $67, $68, $74, $2, $19, $7, $43, $61, $70, $74, $69, $6F, $6E, $6, $2, $55, $70, $8, 
$54, $61, $62, $4F, $72, $64, $65, $72, $2, $3, $7, $4F, $6E, $43, $6C, $69, $63, $6B, $7, $A, 
$62, $74, $6E, $55, $70, $43, $6C, $69, $63, $6B, $0, $0, $7, $54, $42, $69, $74, $42, $74, $6E, 
$7, $62, $74, $6E, $44, $6F, $77, $6E, $4, $4C, $65, $66, $74, $2, $6, $3, $54, $6F, $70, $3, 
$84, $0, $5, $57, $69, $64, $74, $68, $2, $47, $6, $48, $65, $69, $67, $68, $74, $2, $19, $7, 
$43, $61, $70, $74, $69, $6F, $6E, $6, $4, $44, $6F, $77, $6E, $8, $54, $61, $62, $4F, $72, $64, 
$65, $72, $2, $4, $7, $4F, $6E, $43, $6C, $69, $63, $6B, $7, $C, $62, $74, $6E, $44, $6F, $77, 
$6E, $43, $6C, $69, $63, $6B, $0, $0, $0, $0, $0, $9, $54, $54, $61, $62, $53, $68, $65, $65, 
$74, $9, $54, $61, $62, $53, $68, $65, $65, $74, $32, $7, $43, $61, $70, $74, $69, $6F, $6E, $6, 
$9, $43, $6F, $6E, $76, $65, $72, $74, $65, $72, $A, $49, $6D, $61, $67, $65, $49, $6E, $64, $65, 
$78, $2, $1, $0, $6, $54, $4C, $61, $62, $65, $6C, $6, $4C, $61, $62, $65, $6C, $31, $4, $4C, 
$65, $66, $74, $2, $6, $3, $54, $6F, $70, $2, $A, $5, $57, $69, $64, $74, $68, $2, $25, $6, 
$48, $65, $69, $67, $68, $74, $2, $D, $7, $43, $61, $70, $74, $69, $6F, $6E, $6, $7, $53, $6F, 
$75, $72, $63, $65, $3A, $0, $0, $6, $54, $4C, $61, $62, $65, $6C, $6, $4C, $61, $62, $65, $6C,
$32, $4, $4C, $65, $66, $74, $2, $8, $3, $54, $6F, $70, $2, $64, $5, $57, $69, $64, $74, $68, 
$2, $4B, $6, $48, $65, $69, $67, $68, $74, $2, $D, $7, $43, $61, $70, $74, $69, $6F, $6E, $6, 
$F, $43, $6F, $6E, $76, $65, $72, $74, $69, $6F, $6E, $20, $4C, $6F, $67, $3A, $0, $0, $9, $54, 
$43, $6F, $6D, $62, $6F, $42, $6F, $78, $8, $63, $62, $53, $6F, $75, $72, $63, $65, $4, $4C, $65, 
$66, $74, $2, $6, $3, $54, $6F, $70, $2, $1A, $5, $57, $69, $64, $74, $68, $3, $DD, $0, $6, 
$48, $65, $69, $67, $68, $74, $2, $15, $5, $53, $74, $79, $6C, $65, $7, $E, $63, $73, $44, $72,
$6F, $70, $44, $6F, $77, $6E, $4C, $69, $73, $74, $7, $41, $6E, $63, $68, $6F, $72, $73, $B, $6, 
$61, $6B, $4C, $65, $66, $74, $5, $61, $6B, $54, $6F, $70, $7, $61, $6B, $52, $69, $67, $68, $74, 
$0, $A, $49, $74, $65, $6D, $48, $65, $69, $67, $68, $74, $2, $0, $8, $54, $61, $62, $4F, $72,
$64, $65, $72, $2, $0, $0, $0, $7, $54, $42, $75, $74, $74, $6F, $6E, $7, $42, $75, $74, $74, 
$6F, $6E, $31, $4, $4C, $65, $66, $74, $3, $E8, $0, $3, $54, $6F, $70, $2, $1A, $5, $57, $69, 
$64, $74, $68, $2, $37, $6, $48, $65, $69, $67, $68, $74, $2, $15, $7, $43, $61, $70, $74, $69, 
$6F, $6E, $6, $7, $52, $65, $66, $72, $65, $73, $68, $8, $54, $61, $62, $4F, $72, $64, $65, $72, 
$2, $1, $7, $4F, $6E, $43, $6C, $69, $63, $6B, $7, $C, $42, $75, $74, $74, $6F, $6E, $31, $43, 
$6C, $69, $63, $6B, $0, $0, $7, $54, $42, $75, $74, $74, $6F, $6E, $A, $42, $74, $6E, $43, $6F, 
$6E, $76, $65, $72, $74, $4, $4C, $65, $66, $74, $2, $74, $3, $54, $6F, $70, $2, $42, $5, $57, 
$69, $64, $74, $68, $2, $47, $6, $48, $65, $69, $67, $68, $74, $2, $19, $7, $43, $61, $70, $74, 
$69, $6F, $6E, $6, $A, $43, $6F, $6E, $76, $65, $72, $74, $2E, $2E, $2E, $8, $54, $61, $62, $4F, 
$72, $64, $65, $72, $2, $2, $7, $4F, $6E, $43, $6C, $69, $63, $6B, $7, $F, $42, $74, $6E, $43, 
$6F, $6E, $76, $65, $72, $74, $43, $6C, $69, $63, $6B, $0, $0, $5, $54, $4D, $65, $6D, $6F, $5, 
$4D, $65, $6D, $6F, $31, $4, $4C, $65, $66, $74, $2, $6, $3, $54, $6F, $70, $2, $76, $5, $57, 
$69, $64, $74, $68, $3, $17, $1, $6, $48, $65, $69, $67, $68, $74, $3, $D7, $0, $7, $41, $6E, 
$63, $68, $6F, $72, $73, $B, $6, $61, $6B, $4C, $65, $66, $74, $5, $61, $6B, $54, $6F, $70, $7, 
$61, $6B, $52, $69, $67, $68, $74, $8, $61, $6B, $42, $6F, $74, $74, $6F, $6D, $0, $8, $54, $61, 
$62, $4F, $72, $64, $65, $72, $2, $3, $0, $0, $0, $0, $6, $54, $54, $69, $6D, $65, $72, $6, 
$54, $69, $6D, $65, $72, $31, $7, $45, $6E, $61, $62, $6C, $65, $64, $8, $7, $4F, $6E, $54, $69, 
$6D, $65, $72, $7, $B, $54, $69, $6D, $65, $72, $31, $54, $69, $6D, $65, $72, $4, $4C, $65, $66, 
$74, $2, $1F, $3, $54, $6F, $70, $3, $7, $1, $0, $0, $0, $0);

procedure ShowEditForm(const AParentComponent: TComponent; const ARootItem: TTeCustomItem;
    const ADesigner: {$IFDEF KS_COMPILER6_UP} IDesigner); {$ELSE} IFormDesigner); {$ENDIF}
var
  i: integer;
  Form: TCustomForm;
  EditForm: TfrmMenuDesignerForm;
begin
  for i := 0 to Screen.FormCount-1 do
  begin
    Form := Screen.Forms[I];
    if Form is TfrmMenuDesignerForm then
      if TfrmMenuDesignerForm(Form).FRootItem = ARootItem then
      begin
        Form.Show;
        if Form.WindowState = wsMinimized then
          Form.WindowState := wsNormal;
        Exit;
      end;
  end;

  EditForm := TfrmMenuDesignerForm.CreateForm;
  try
    EditForm.Designer := ADesigner;
    EditForm.FParentComponent := AParentComponent;
    AParentComponent.FreeNotification(EditForm);
    EditForm.FRootItem := ARootItem;
    EditForm.FRootItem.OnInternalChange := EditForm.DoItemsChange;
    EditForm.FSelRootItem := ARootItem;
    EditForm.FSelItem := ARootItem;

    EditForm.Caption := 'Editing ' + AParentComponent.Name;
    EditForm.ItemsName.Caption := ' ' + AParentComponent.Name + '.Items';

    EditForm.RebuildTreeItem;
    EditForm.ActiveControl := EditForm.TreeItems;

    EditForm.Show;
  except
    EditForm.Free;
    raise;
  end;
end;

{ TfrmMenuDesigner }

constructor TfrmMenuDesignerForm.CreateForm;
var
  F: TForm;
  M: TMemoryStream;
begin
  inherited CreateNew(Application);

  { Load from  }
  M := TMemoryStream.Create;
  try
    M.Write(MenuDesignerRes, SizeOf(MenuDesignerRes));
    M.Seek(0, soFromBeginning);

    M.ReadResHeader;

    M.ReadComponent(Self);
  finally
    M.Free;
  end;
end;

destructor TfrmMenuDesignerForm.Destroy;
begin
  inherited;
end;

procedure TfrmMenuDesignerForm.FormCreate(Sender: TObject);
begin
  { Create }
  Button1Click(Self);
end;

procedure TfrmMenuDesignerForm.PagesChange(Sender: TObject);
begin
  if Pages.ActivePage.TabIndex = 1 then
    Button1Click(Self);
end;

procedure TfrmMenuDesignerForm.Button1Click(Sender: TObject);
var
  Form: TCustomForm;
  i: integer;
begin
  if FParentComponent = nil then Exit;
  if not (FParentComponent.Owner is TCustomForm) then Exit;

  { Rebuild Source List }

  cbSource.Items.Clear;
  Form := TCustomForm(FParentComponent.Owner);

  for i := 0 to Form.ComponentCount - 1 do
  begin
    if Form.Components[i] is TTeCustomPopupMenu then Continue;

    if Form.Components[i] is TMainMenu then
      cbSource.Items.Add(Form.Components[i].Name);

    if Form.Components[i] is TPopupMenu then
      cbSource.Items.Add(Form.Components[i].Name);
  end;
end;

procedure TfrmMenuDesignerForm.FormClose(Sender: TObject;
  var Action: TCloseAction);
begin
  if Designer <> nil then Designer.Modified;

  if FParentComponent <> nil then
    if FParentComponent is TWinControl then
      (FParentComponent as TWinControl).Invalidate;
  { Destroy }
  Action := caFree;
end;

procedure TfrmMenuDesignerForm.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and
     ((AComponent = FParentComponent) or (AComponent = FRootItem)) then
    Free;
end;

function TfrmMenuDesignerForm.UniqueName(Component: TComponent): String;
var
  S: String;
begin
  S := Component.ClassName;
  if (Length(S) > 1) and (UpCase(S[1]) = 'T') then
    System.Delete (S, 1, 1);
  Result := Designer.UniqueName(S);
end;

procedure TfrmMenuDesignerForm.DoItemsChange(Sender: TObject);
begin
  { Update if items Change }
  RebuildTreeItem;
  
  if FParentComponent <> nil then
    if FParentComponent is TWinControl then
    begin
      (FParentComponent as TWinControl).BoundsRect := (FParentComponent as TWinControl).BoundsRect;
      (FParentComponent as TWinControl).Invalidate;
    end;

  if Designer <> nil then Designer.Modified;
end;

procedure TfrmMenuDesignerForm.RebuildTreeItem;
  procedure AddItems(Parent: TTeCustomItem; TreeNode: TTreeNode);
  var
    i: integer;
    Node: TTreeNode;
  begin
    for i := 0 to Parent.Count-1 do
    begin
      Node := TreeItems.Items.AddChild(TreeNode, Parent[i].Caption);
      Node.Data := Parent[i];
      if FSelItem = Parent[i] then
        Node.Selected := true;
      if Parent[i].Count > 0 then
      begin
        AddItems(Parent[i], Node);
      end;
    end;
  end;

begin
  if FRootItem = nil then Exit;

  TreeItems.Items.BeginUpdate;
  try
    TreeItems.Items.Clear;

    AddItems(FRootItem, nil);
  finally
    TreeItems.Items.EndUpdate;
  end;
end;

procedure TfrmMenuDesignerForm.InsertNewItem;
var
  ItemClass: TTeCustomItemClass;
  Item: TTeCustomItem;
begin
  { Get Item Class }
  ItemClass := GetItemClass(FParentComponent.ClassType);
  if ItemClass <> nil then
  begin
    { Add Item }
    Item := ItemClass.Create(FParentComponent.Owner);
    try
      if Designer <> nil then
        Item.Name := Designer.UniqueName(SDefaultMenuItemName);

      Item.Caption := Item.Name;

      if FSelRootItem <> nil then
      begin
        FSelRootItem.Add(Item);
      end;

      FSelItem := Item;
    except
      raise;
    end;
    Designer.Modified;

    RebuildTreeItem;

    ActiveControl := TreeItems;
  end;
end;

procedure TfrmMenuDesignerForm.TreeItemsClick(Sender: TObject);
begin
  { Select Item }
  if TreeItems.Selected = nil then Exit;
  if TreeItems.Selected.Data = nil then Exit;

  FSelItem := TTeCustomItem(TreeItems.Selected.Data);

  if (Designer <> nil) and (FSelItem <> nil) and not(csDestroying in FSelItem.ComponentState) then
    Designer.SelectComponent(FSelItem);
end;

procedure TfrmMenuDesignerForm.Timer1Timer(Sender: TObject);
begin
  Exit;
  { Check Enabled State }
  btnDelete.Enabled := not (FSelItem = nil);
  btnInsert.Enabled := not (FSelRootItem = nil);
  btnSubMenu.Enabled := not (FSelRootItem = nil);

  btnUp.Enabled := not ((FSelItem = nil) and (FSelItem.Parent = nil) and
    (FSelItem.Parent.IndexOf(FSelItem) = 0));
  btnDown.Enabled := not ((FSelItem = nil) and (FSelItem.Parent = nil) and
    (FSelItem.Parent.IndexOf(FSelItem) = FSelItem.Parent.Count-1));
end;

procedure TfrmMenuDesignerForm.btnInsertClick(Sender: TObject);
begin
  { Insert Item }
  if FSelItem.Parent <> nil then
    FSelRootItem := FSelItem.Parent;
  InsertNewItem;
end;

procedure TfrmMenuDesignerForm.btnSubMenuClick(Sender: TObject);
begin
  { Insert Child }
  if TreeItems.Selected = nil then Exit;
  if TreeItems.Selected.Data = nil then Exit;

  FSelRootItem := TTeCustomItem(TreeItems.Selected.Data);
  InsertNewItem;
end;

procedure TfrmMenuDesignerForm.btnDeleteClick(Sender: TObject);
var
  Index: integer;
  ParentItem: TTeCustomItem;
begin
  { Delete Item }
  if FSelItem = nil then Exit;
  if FSelItem.Parent = nil then Exit;

  ParentItem := FSelItem.Parent;

  Index := ParentItem.IndexOf(FSelItem); 
  ParentItem.Remove(FSelItem);
  FSelItem.Free;

  if (Index > 0) and (ParentItem.Count > 0) then
    FSelItem := ParentItem[Index-1]
  else
    if (ParentItem.Count > 0) then
      FSelItem := ParentItem[0];

  RebuildTreeItem;

  ActiveControl := TreeItems;
end;

procedure TfrmMenuDesignerForm.btnUpClick(Sender: TObject);
var
  Index: integer;
  ParentItem: TTeCustomItem;
begin
  { Up }
  if FSelItem = nil then Exit;
  if FSelItem.Parent = nil then Exit;
  if FSelItem.Parent.IndexOf(FSelItem) = 0 then Exit;

  ParentItem := FSelItem.Parent;

  Index := ParentItem.IndexOf(FSelItem);
  Dec(Index);
  ParentItem.Remove(FSelItem);
  ParentItem.Insert(Index, FSelItem);

  FSelItem := FSelItem;

  RebuildTreeItem;

  ActiveControl := TreeItems;
end;

procedure TfrmMenuDesignerForm.btnDownClick(Sender: TObject);
var
  Index: integer;
  ParentItem: TTeCustomItem;
begin
  { Down }
  if FSelItem = nil then Exit;
  if FSelItem.Parent = nil then Exit;
  if FSelItem.Parent.IndexOf(FSelItem) = FSelItem.Parent.Count-1 then Exit;

  ParentItem := FSelItem.Parent;

  Index := ParentItem.IndexOf(FSelItem);
  Inc(Index);
  ParentItem.Remove(FSelItem);
  ParentItem.Insert(Index, FSelItem);

  FSelItem := FSelItem;

  RebuildTreeItem;

  ActiveControl := TreeItems;
end;

procedure TfrmMenuDesignerForm.TreeItemsEdited(Sender: TObject; Node: TTreeNode;
  var S: String);
var
  Item: TTeCustomItem;
begin
  { Edit caption }
  if Node = nil then Exit;
  if Node.Data = nil then Exit;

  Item := TTeCustomItem(Node.Data);
  Item.Caption := S;

  Designer.Modified;
end;

procedure TfrmMenuDesignerForm.TreeItemsDblClick(Sender: TObject);
var
  SelItem: TTeCustomItem;
  PropCount, I: Integer;
  Props: PPropList;
  PropInfo: PPropInfo;
  MethodName: String;
  Method: TMethod;
begin
  { Add method }
  SelItem := nil;
  if Assigned(TreeItems.Selected) then
    SelItem := TreeItems.Selected.Data;

  if SelItem = nil then Exit;

  PropCount := GetPropList(SelItem.ClassInfo, [tkMethod], nil);
  GetMem(Props, PropCount * SizeOf(PPropInfo));
  try
    GetPropList(FSelItem.ClassInfo, [tkMethod], Props);
    for I := PropCount-1 downto 0 do begin
      PropInfo := Props[I];
      if CompareText(PropInfo.Name, 'OnClick') = 0 then begin
        Method := GetMethodProp(SelItem, PropInfo);
        MethodName := Designer.GetMethodName(Method);
        if MethodName = '' then
        begin
          MethodName := SelItem.Name + 'Click';
          Method := Designer.CreateMethod(MethodName, GetTypeData(PropInfo.PropType^));
          SetMethodProp (SelItem, {$IFDEF KS_D5} PropInfo.Name {$ELSE} PropInfo {$ENDIF},
            Method);
          Designer.Modified;
        end;
        if Designer.MethodExists(MethodName) then
          Designer.ShowMethod (MethodName);
        Break;
      end;
    end;
  finally
    FreeMem (Props);
  end;
end;

procedure TfrmMenuDesignerForm.BtnConvertClick(Sender: TObject);
var
  Form: TCustomForm;
  SourceItems: TMenuItem;
  Source: TComponent;
begin
  if FRootItem = nil then Exit;
  if cbSource.ItemIndex < 0 then Exit;
  if cbSource.Items.Count = 0 then Exit;
  if FParentComponent = nil then Exit;
  if not (FParentComponent.Owner is TCustomForm) then
    raise Exception.Create('The converter can only be used on a form');

  Form := TCustomForm(FParentComponent.Owner);

  Source := Form.FindComponent(cbSource.Items[cbSource.ItemIndex]);

  if Source <> nil then
  begin
    if MessageDlg('Convert items from '+(Source as TMenu).Name+'?', mtConfirmation, [mbYes, mbNo, mbCancel], 0) <> mrYes then
      Exit;

    if Source is TMenu then
    begin
      DoConvert(FRootItem, Source as TMenu, FParentComponent.Owner,
        GetItemClass(FParentComponent.ClassType), Memo1.Lines);
    end;

    ShowMessage('Convertion complete!');

    RebuildTreeItem;
  end;
end;



{ IDE Editors =================================================================}

{ TTeMenuBarEditor ============================================================}

procedure TTeMenuBarEditor.Edit;
begin
  if Assigned(Component) then
    ShowEditForm(Component, TTeCustomMenuBar(Component).Items, Designer);
end;

procedure TTeMenuBarEditor.ExecuteVerb (Index: Integer);
begin
  if Index = 0 then
    Edit;
end;

function TTeMenuBarEditor.GetVerbCount: Integer;
begin
  Result := 1;
end;

function TTeMenuBarEditor.GetVerb(Index: Integer): String;
begin
  if Index = 0 then
    Result := 'Edit...'
  else
    Result := '';
end;

{ TTePopupMenuEditor }

procedure TTePopupMenuEditor.Edit;
begin
  if Assigned(Component) then
    ShowEditForm(Component, TTeCustomPopupMenu(Component).Items, Designer);
end;

procedure TTePopupMenuEditor.ExecuteVerb (Index: Integer);
begin
  if Index = 0 then
    Edit;
end;

function TTePopupMenuEditor.GetVerbCount: Integer;
begin
  Result := 1;
end;

function TTePopupMenuEditor.GetVerb(Index: Integer): String;
begin
  if Index = 0 then
    Result := 'Edit...'
  else
    Result := '';
end;

{ TTeItemsPropertyEditor }

procedure TTeItemsPropertyEditor.Edit;
var
  Editor: {$IFDEF KS_COMPILER6_UP} IComponentEditor; {$ELSE} TComponentEditor; {$ENDIF}
begin
  if PropCount <> 1 then Exit;
  Editor := GetComponentEditor(GetComponent(0) as TComponent, Designer);
  try
    Editor.Edit;
  finally
    {$IFNDEF KS_COMPILER6_UP}
    Editor.Free;
    {$ENDIF}
  end;
end;

function TTeItemsPropertyEditor.GetAttributes: TPropertyAttributes;
begin
  Result := inherited GetAttributes + [paDialog, paReadOnly];
end;

function TTeItemsPropertyEditor.GetValue: String;
begin
  Result := '(Items)';
end;

end.
