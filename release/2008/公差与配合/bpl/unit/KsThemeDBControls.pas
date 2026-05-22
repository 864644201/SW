{==============================================================================

  ThemeEngine's DB Grids
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: KsThemeDBControls.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemedbcontrols;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses Windows, SysUtils, Messages, Classes, Controls, Forms, Graphics, Menus,
   StdCtrls, ExtCtrls, Buttons, ComCtrls, DB, DBCtrls, KsThemeEdits,
   te_controls, KsThemeCheckBoxs, KsThemeComboBoxs, KsThemeListBoxs, KsThemeLabels,
   KsThemeGroupBoxs, KsThemeScrollBars, KsThemeThemes, KsThemeEngine,
   KsThemeStdControl, KsThemeInplaceEdit, KsThemeVersion;

type

{ TTeThemeDBText }

  TTeThemeDBText = class(TTeThemeLabel)
  private
    FDataLink: TFieldDataLink;
    procedure DataChange(Sender: TObject);
    function GetDataField: string;
    function GetDataSource: TDataSource;
    function GetField: TField;
    function GetFieldText: string;
    procedure SetDataField(const Value: string);
    procedure SetDataSource(Value: TDataSource);
    procedure CMGetDataLink(var Message: TMessage); message CM_GETDATALINK;
  protected
    function GetLabelText: WideString; override;
    procedure Notification(AComponent: TComponent;
      Operation: TOperation); override;
    procedure SetAutoSize(Value: Boolean); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;

    function ExecuteAction(Action: TBasicAction): Boolean; override;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    function UseRightToLeftAlignment: Boolean; override;
    property Field: TField read GetField;
  published
    property AutoSize default False;
    property DataField: string read GetDataField write SetDataField;
    property DataSource: TDataSource read GetDataSource write SetDataSource;
  end; 

{ TTeThemeDBEdit }

  TTeThemeDBEdit = class(TTeThemeEdit)
  private
    FDataLink: TFieldDataLink;
    FDataChanging: boolean;
    procedure ActiveChange(Sender: TObject);
    procedure DataChange(Sender: TObject);
    procedure EditingChange(Sender: TObject);
    function GetDataField: string;
    function GetDataSource: TDataSource;
    function GetField: TField;
    function GetReadOnly: Boolean;
    procedure ResetMaxLength;
    procedure SetDataField(const Value: string);
    procedure SetDataSource(Value: TDataSource);
    procedure SetReadOnly(Value: Boolean);
    procedure UpdateData(Sender: TObject);
    procedure WMCut(var Message: TMessage); message WM_CUT;
    procedure WMPaste(var Message: TMessage); message WM_PASTE;
    procedure WMUndo(var Message: TMessage); message WM_UNDO;
    procedure CMGetDataLink(var Message: TMessage); message CM_GETDATALINK;
  protected
    procedure Change; override;
    procedure KeyDown(var Key: Word; Shift: TShiftState); override;
    procedure KeyPress(var Key: Char); override;
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
    procedure HasFocus; override;
    procedure KillFocus; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;

    function ExecuteAction(Action: TBasicAction): Boolean; override;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    function UseRightToLeftAlignment: Boolean; override;
    property Field: TField read GetField;
  published
    property Anchors;
    property AutoSelect;
    property AutoSize;
    property BiDiMode;
    property CharCase;
    property Color;
    property Constraints;
    property Ctl3D;
    property DataField: string read GetDataField write SetDataField;
    property DataSource: TDataSource read GetDataSource write SetDataSource;
    property DragCursor;
    property DragKind;
    property DragMode;
    property Enabled;
    property Font;
    property ImeMode;
    property ImeName;
    property MaxLength;
    property ParentBiDiMode;
    property ParentColor;
    property ParentCtl3D;
    property ParentFont;
    property ParentShowHint;
    property PopupMenu;
    property ReadOnly: Boolean read GetReadOnly write SetReadOnly default False;
    property ShowHint;
    property TabOrder;
    property TabStop;
    property Visible;
    property OnChange;
    property OnClick;
    property OnDblClick;
    property OnDragDrop;
    property OnDragOver;
    property OnEndDock;
    property OnEndDrag;
    property OnEnter;
    property OnExit;
    property OnKeyDown;
    property OnKeyPress;
    property OnKeyUp;
    property OnMouseDown;
    property OnMouseMove;
    property OnMouseUp;
    property OnStartDock;
    property OnStartDrag;
  end;

{ TTeThemeDBMaskEdit }

  TTeThemeDBMaskEdit = class(TTeThemeMaskEdit)
  private
    FDataLink: TFieldDataLink;
    FCanvas: TControlCanvas;
    FAlignment: TAlignment;
    FFocused: Boolean;
    procedure ActiveChange(Sender: TObject);
    procedure DataChange(Sender: TObject);
    procedure EditingChange(Sender: TObject);
    function GetDataField: string;
    function GetDataSource: TDataSource;
    function GetField: TField;
    function GetReadOnly: Boolean;
    function GetTextMargins: TPoint;
    procedure ResetMaxLength;
    procedure SetDataField(const Value: string);
    procedure SetDataSource(Value: TDataSource);
    procedure SetFocused(Value: Boolean);
    procedure SetReadOnly(Value: Boolean);
    procedure UpdateData(Sender: TObject);
    procedure WMCut(var Message: TMessage); message WM_CUT;
    procedure WMPaste(var Message: TMessage); message WM_PASTE;
    procedure WMUndo(var Message: TMessage); message WM_UNDO;
    procedure CMEnter(var Message: TCMEnter); message CM_ENTER;
    procedure CMExit(var Message: TCMExit); message CM_EXIT;
    procedure WMPaint(var Message: TWMPaint); message WM_PAINT;
    procedure CMGetDataLink(var Message: TMessage); message CM_GETDATALINK;
  protected
    procedure Change; override;
    function EditCanModify: Boolean; override;
    procedure KeyDown(var Key: Word; Shift: TShiftState); override;
    procedure KeyPress(var Key: Char); override;
    procedure Loaded; override;
    procedure Notification(AComponent: TComponent;
      Operation: TOperation); override;
    procedure Reset; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    function ExecuteAction(Action: TBasicAction): Boolean; override;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    function UseRightToLeftAlignment: Boolean; override;
    property Field: TField read GetField;
  published
    property Anchors;
    property AutoSelect;
    property AutoSize;
    property BiDiMode;
    property BorderStyle;
    property CharCase;
    property Color;
    property Constraints;
    property Ctl3D;
    property DataField: string read GetDataField write SetDataField;
    property DataSource: TDataSource read GetDataSource write SetDataSource;
    property DragCursor;
    property DragKind;
    property DragMode;
    property Enabled;
    property Font;
    property ImeMode;
    property ImeName;
    property MaxLength;
    property ParentBiDiMode;
    property ParentColor;
    property ParentCtl3D;
    property ParentFont;
    property ParentShowHint;
    property PasswordChar;
    property PopupMenu;
    property ReadOnly: Boolean read GetReadOnly write SetReadOnly default False;
    property ShowHint;
    property TabOrder;
    property TabStop;
    property Visible;
    property OnChange;
    property OnClick;
    property OnDblClick;
    property OnDragDrop;
    property OnDragOver;
    property OnEndDock;
    property OnEndDrag;
    property OnEnter;
    property OnExit;
    property OnKeyDown;
    property OnKeyPress;
    property OnKeyUp;
    property OnMouseDown;
    property OnMouseMove;
    property OnMouseUp;
    property OnStartDock;
    property OnStartDrag;
  end;

{ TTeThemeDBMemo }

  TTeThemeDBMemo = class(TteThemeMemo)
  private
    FDataLink: TFieldDataLink;
    FAutoDisplay: Boolean;
    FFocused: Boolean;
    FMemoLoaded: Boolean;
    FPaintControl: TPaintControl;
    procedure DataChange(Sender: TObject);
    procedure EditingChange(Sender: TObject);
    function GetDataField: string;
    function GetDataSource: TDataSource;
    function GetField: TField;
    function GetReadOnly: Boolean;
    procedure SetDataField(const Value: string);
    procedure SetDataSource(Value: TDataSource);
    procedure SetReadOnly(Value: Boolean);
    procedure SetAutoDisplay(Value: Boolean);
    procedure SetFocused(Value: Boolean);
    procedure UpdateData(Sender: TObject);
    procedure WMCut(var Message: TMessage); message WM_CUT;
    procedure WMPaste(var Message: TMessage); message WM_PASTE;
    procedure WMUndo(var Message: TMessage); message WM_UNDO;
    procedure CMEnter(var Message: TCMEnter); message CM_ENTER;
    procedure CMExit(var Message: TCMExit); message CM_EXIT;
    procedure WMLButtonDblClk(var Message: TWMLButtonDblClk); message WM_LBUTTONDBLCLK;
    procedure CMGetDataLink(var Message: TMessage); message CM_GETDATALINK;
  protected
    procedure Change; override;
    procedure KeyDown(var Key: Word; Shift: TShiftState); override;
    procedure KeyPress(var Key: Char); override;
    procedure Loaded; override;
    procedure Notification(AComponent: TComponent;
      Operation: TOperation); override;
    procedure WndProc(var Message: TMessage); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    function ExecuteAction(Action: TBasicAction): Boolean; override;
    procedure LoadMemo; virtual;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    function UseRightToLeftAlignment: Boolean; override;
    property Field: TField read GetField;
  published
    property Align;
    property Alignment;
    property Anchors;
    property AutoDisplay: Boolean read FAutoDisplay write SetAutoDisplay default True;
    property BiDiMode;
    property BorderStyle;
    property Color;
    property Constraints;
    property Ctl3D;
    property DataField: string read GetDataField write SetDataField;
    property DataSource: TDataSource read GetDataSource write SetDataSource;
    property DragCursor;
    property DragKind;
    property DragMode;
    property Enabled;
    property Font;
    property ImeMode;
    property ImeName;
    property MaxLength;
    property ParentBiDiMode;
    property ParentColor;
    property ParentCtl3D;
    property ParentFont;
    property ParentShowHint;
    property PopupMenu;
    property ReadOnly: Boolean read GetReadOnly write SetReadOnly default False;
    property ScrollBars;
    property ShowHint;
    property TabOrder;
    property TabStop;
    property Visible;
    property WantTabs;
    property WordWrap;
    property OnChange;
    property OnClick;
    property OnDblClick;
    property OnDragDrop;
    property OnDragOver;
    property OnEndDock;
    property OnEndDrag;
    property OnEnter;
    property OnExit;
    property OnKeyDown;
    property OnKeyPress;
    property OnKeyUp;
    property OnMouseDown;
    property OnMouseMove;
    property OnMouseUp;
    property OnStartDock;
    property OnStartDrag;
  end;

{ TTeThemeDBCheckBox }

  TTeThemeDBCheckBox = class(TTeThemeCheckBox)
  private
    FDataLink: TFieldDataLink;
    FValueCheck: string;
    FValueUncheck: string;
    FDataChanging: boolean;
    procedure DataChange(Sender: TObject);
    function GetDataField: string;
    function GetDataSource: TDataSource;
    function GetField: TField;
    function GetFieldState: TCheckBoxState;
    function GetReadOnly: Boolean;
    procedure SetDataField(const Value: string);
    procedure SetDataSource(Value: TDataSource);
    procedure SetReadOnly(Value: Boolean);
    procedure SetValueCheck(const Value: string);
    procedure SetValueUncheck(const Value: string);
    procedure UpdateData(Sender: TObject);
    function ValueMatch(const ValueList, Value: string): Boolean;
  protected
    procedure CMGetDataLink(var Message: TMessage); message CM_GETDATALINK;

    procedure KillFocus; override;
    procedure Click; override;
    procedure Toggle; override;
    procedure KeyPress(var Key: Char); override;
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    function ExecuteAction(Action: TBasicAction): Boolean; override;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    function UseRightToLeftAlignment: Boolean; override;
    property Checked;
    property Field: TField read GetField;
    property State;
  published
    property DataField: string read GetDataField write SetDataField;
    property DataSource: TDataSource read GetDataSource write SetDataSource;
    property ReadOnly: Boolean read GetReadOnly write SetReadOnly default False;
    property ValueChecked: string read FValueCheck write SetValueCheck;
    property ValueUnchecked: string read FValueUncheck write SetValueUncheck;
  end;

{ TTeThemeDBComboBox }

  TTeThemeDBComboBox = class(TTeThemeComboBox)
  private
    FDataLink: TFieldDataLink;
    FPaintControl: TPaintControl;
    FDataChanging: boolean;
    procedure DataChange(Sender: TObject);
    procedure EditingChange(Sender: TObject);
    function GetComboText: string;
    function GetDataField: string;
    function GetDataSource: TDataSource;
    function GetField: TField;
    function GetReadOnly: Boolean;
    procedure SetComboText(const Value: string);
    procedure SetDataField(const Value: string);
    procedure SetDataSource(Value: TDataSource);
    procedure SetEditReadOnly;
    procedure SetItems(Value: TStrings);
    procedure SetReadOnly(Value: Boolean);
    procedure UpdateData(Sender: TObject);
    procedure CMGetDataLink(var Message: TMessage); message CM_GETDATALINK;
  protected
    procedure HasFocus; override;
    procedure KillFocus; override;
    procedure Change; override;
    procedure Click; override;
    procedure CreateWnd; override;
    procedure KeyDown(var Key: Word; Shift: TShiftState); override;
    procedure KeyPress(var Key: Char); override;
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;

    function ExecuteAction(Action: TBasicAction): Boolean; override;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    function UseRightToLeftAlignment: Boolean; override;
    property Field: TField read GetField;
    property Text;
  published
    property ComboStyle;
    property DataField: string read GetDataField write SetDataField;
    property DataSource: TDataSource read GetDataSource write SetDataSource;
    property Items write SetItems;
    property ReadOnly: Boolean read GetReadOnly write SetReadOnly default False;
  end;

{ TTeThemeDBListBox }

  TTeThemeDBListBox = class(TTeThemeListBox)
  private
    FDataLink: TFieldDataLink;
    FDataChanging: boolean;
    procedure DataChange(Sender: TObject);
    procedure UpdateData(Sender: TObject);
    function GetDataField: string;
    function GetDataSource: TDataSource;
    function GetField: TField;
    function GetReadOnly: Boolean;
    procedure SetDataField(const Value: string);
    procedure SetDataSource(Value: TDataSource);
    procedure SetReadOnly(Value: Boolean);
    procedure SetItems(Value: TStrings);
    procedure WMLButtonDown(var Message: TWMLButtonDown); message WM_LBUTTONDOWN;
  protected
    procedure KillFocus; override;
    procedure Click; override;
    procedure KeyDown(var Key: Word; Shift: TShiftState); override;
    procedure KeyPress(var Key: Char); override;
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    function ExecuteAction(Action: TBasicAction): Boolean; override;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    function UseRightToLeftAlignment: Boolean; override;
    property Field: TField read GetField;
  published
    property DataField: string read GetDataField write SetDataField;
    property DataSource: TDataSource read GetDataSource write SetDataSource;
    property Items write SetItems;
    property ReadOnly: Boolean read GetReadOnly write SetReadOnly default False;
  end;

{ TTeThemeDBRadioGroup }

  TTeThemeDBRadioGroup = class(TTeThemeRadioGroup)
  private
    FDataLink: TFieldDataLink;
    FValue: string;
    FValues: TStrings;
    FInSetValue: Boolean;
    FOnChange: TNotifyEvent;
    procedure DataChange(Sender: TObject);
    procedure UpdateData(Sender: TObject);
    function GetDataField: string;
    function GetDataSource: TDataSource;
    function GetField: TField;
    function GetReadOnly: Boolean;
    function GetButtonValue(Index: Integer): string;
    procedure SetDataField(const Value: string);
    procedure SetDataSource(Value: TDataSource);
    procedure SetReadOnly(Value: Boolean);
    procedure SetValue(const Value: string);
    procedure SetItems(Value: TStrings);
    procedure SetValues(Value: TStrings);
  protected
    procedure KillFocus; override;
    procedure Change; dynamic;
    procedure Click; override;
    procedure KeyPress(var Key: Char); override;
    function CanModify: Boolean; override;
    procedure Notification(AComponent: TComponent;
      Operation: TOperation); override;
    property DataLink: TFieldDataLink read FDataLink;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    function ExecuteAction(Action: TBasicAction): Boolean; override;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    function UseRightToLeftAlignment: Boolean; override;
    property Field: TField read GetField;
    property ItemIndex;
    property Value: string read FValue write SetValue;
  published
    property DataField: string read GetDataField write SetDataField;
    property DataSource: TDataSource read GetDataSource write SetDataSource;
    property Items write SetItems;
    property ReadOnly: Boolean read GetReadOnly write SetReadOnly default False;
    property Values: TStrings read FValues write SetValues;
    property OnChange: TNotifyEvent read FOnChange write FOnChange;
  end;

{ TTeDBLookupControl }

  TTeDBLookupControl = class;

  TTeDataSourceLink = class(TDataLink)
  private
    FDBLookupControl: TTeDBLookupControl;
  protected
    procedure FocusControl(Field: TFieldRef); override;
    procedure ActiveChanged; override;
    procedure LayoutChanged; override;
    procedure RecordChanged(Field: TField); override;
  public
    constructor Create;
  end;

  TTeListSourceLink = class(TDataLink)
  private
    FDBLookupControl: TTeDBLookupControl;
  protected
    procedure ActiveChanged; override;
    procedure DataSetChanged; override;
    procedure LayoutChanged; override;
  public
    constructor Create;
  end;

  TTeDBLookupControl = class(TTeCustomControl)
  private
    FLookupSource: TDataSource;
    FDataLink: TTeDataSourceLink;
    FListLink: TTeListSourceLink;
    FDataFieldName: string;
    FKeyFieldName: string;
    FListFieldName: string;
    FListFieldIndex: Integer;
    FDataField: TField;
    FMasterField: TField;
    FKeyField: TField;
    FListField: TField;
    FListFields: TList;
    FKeyValue: Variant;
    FSearchText: string;
    FLookupMode: Boolean;
    FListActive: Boolean;
    FHasFocus: Boolean;
    FThemeEngine: TTeThemeEngine;
    FClearKey: TShortCut;
    procedure CheckNotCircular;
    procedure CheckNotLookup;
    procedure DataLinkRecordChanged(Field: TField);
    function GetDataSource: TDataSource;
    function GetKeyFieldName: string;
    function GetListSource: TDataSource;
    function GetReadOnly: Boolean;
    procedure SetDataFieldName(const Value: string);
    procedure SetDataSource(Value: TDataSource);
    procedure SetKeyFieldName(const Value: string);
    procedure SetKeyValue(const Value: Variant);
    procedure SetListFieldName(const Value: string);
    procedure SetListSource(Value: TDataSource);
    procedure SetLookupMode(Value: Boolean);
    procedure SetReadOnly(Value: Boolean);
    procedure WMGetDlgCode(var Message: TMessage); message WM_GETDLGCODE;
    procedure WMKillFocus(var Message: TMessage); message WM_KILLFOCUS;
    procedure WMSetFocus(var Message: TMessage); message WM_SETFOCUS;
    procedure CMEnabledChanged(var Message: TMessage); message CM_ENABLEDCHANGED;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    procedure KeyDown(var Key: Word; Shift: TShiftState); override;
    procedure PaintBorder; override;
    function CanModify: Boolean; virtual;
    function GetBorderSize: Integer; virtual;
    function GetTextHeight: Integer; virtual;
    procedure KeyValueChanged; virtual;
    procedure ListLinkDataChanged; virtual;
    function LocateKey: Boolean; virtual;
    procedure Notification(AComponent: TComponent;
      Operation: TOperation); override;
    procedure ProcessSearchKey(Key: Char); virtual;
    procedure SelectKeyValue(const Value: Variant); virtual;
    procedure UpdateDataFields; virtual;
    procedure UpdateListFields; virtual;
    property DataField: string read FDataFieldName write SetDataFieldName;
    property DataSource: TDataSource read GetDataSource write SetDataSource;
    property HasFocus: Boolean read FHasFocus;
    property KeyField: string read GetKeyFieldName write SetKeyFieldName;
    property KeyValue: Variant read FKeyValue write SetKeyValue;
    property ListActive: Boolean read FListActive;
    property ListField: string read FListFieldName write SetListFieldName;
    property ListFieldIndex: Integer read FListFieldIndex write FListFieldIndex default 0;
    property ListFields: TList read FListFields;
    property ListLink: TTeListSourceLink read FListLink;
    property ListSource: TDataSource read GetListSource write SetListSource;
    property ParentColor default False;
    property ReadOnly: Boolean read GetReadOnly write SetReadOnly default False;
    property SearchText: string read FSearchText write FSearchText;
    property TabStop default True;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;
    function ExecuteAction(Action: TBasicAction): Boolean; override;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    property Field: TField read FDataField;
  published
    property BevelSides;
    property BevelInner;
    property BevelOuter;
    property BevelKind;
    property BevelWidth;
    property BorderWidth;
    property ClearKey: TShortCut read FClearKey write FClearKey default 0;
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
  end;

{ TTeThemeDBLookupListBox }

  TTeThemeDBLookupListBox = class(TTeDBLookupControl)
  private
    FRecordIndex: Integer;
    FRecordCount: Integer;
    FRowCount: Integer;
    FPopup: Boolean;
    FKeySelected: Boolean;
    FTracking: Boolean;
    FTimerActive: Boolean;
    FLockPosition: Boolean;
    FMousePos: Integer;
    FSelectedItem: string;
    function GetKeyIndex: Integer;
    procedure SelectCurrent;
    procedure SelectItemAt(X, Y: Integer);
    procedure SetRowCount(Value: Integer);
    procedure StopTimer;
    procedure StopTracking;
    procedure TimerScroll;
    procedure UpdateScrollBar;
    procedure CMFontChanged(var Message: TMessage); message CM_FONTCHANGED;
    procedure WMCancelMode(var Message: TMessage); message WM_CANCELMODE;
    procedure WMTimer(var Message: TMessage); message WM_TIMER;
    procedure WMVScroll(var Message: TWMVScroll); message WM_VSCROLL;
  protected
    procedure CreateParams(var Params: TCreateParams); override;
    procedure CreateWnd; override;
    function CreateScrollBar: TControl; override;
    procedure KeyDown(var Key: Word; Shift: TShiftState); override;
    procedure KeyPress(var Key: Char); override;
    procedure KeyValueChanged; override;
    procedure ListLinkDataChanged; override;
    procedure MouseDown(Button: TMouseButton; Shift: TShiftState;
      X, Y: Integer); override;
    procedure MouseMove(Shift: TShiftState; X, Y: Integer); override;
    procedure MouseUp(Button: TMouseButton; Shift: TShiftState;
      X, Y: Integer); override;
    procedure PaintBuffer; override;
    procedure UpdateListFields; override;
  public
    constructor Create(AOwner: TComponent); override;
    function ExecuteAction(Action: TBasicAction): Boolean; override;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    function UseRightToLeftAlignment: Boolean; override;
    property KeyValue;
    property SelectedItem: string read FSelectedItem;
    procedure SetBounds(ALeft, ATop, AWidth, AHeight: Integer); override;
  published
    property Align;
    property Anchors;
    property BiDiMode;
    property Color;
    property Constraints;
    property Ctl3D;
    property DataField;
    property DataSource;
    property DragCursor;
    property DragKind;
    property DragMode;
    property Enabled;
    property Font;
    property ImeMode;
    property ImeName;
    property KeyField;
    property ListField;
    property ListFieldIndex;
    property ListSource;
    property ParentBiDiMode;
    property ParentColor;
    property ParentCtl3D;
    property ParentFont;
    property ParentShowHint;
    property PopupMenu;
    property ReadOnly;
    property RowCount: Integer read FRowCount write SetRowCount stored False;
    property ShowHint;
    property TabOrder;
    property TabStop;
    property Visible;
    property OnClick;
    property OnDblClick;
    property OnDragDrop;
    property OnDragOver;
    property OnEndDock;
    property OnEndDrag;
    property OnEnter;
    property OnExit;
    property OnKeyDown;
    property OnKeyPress;
    property OnKeyUp;
    property OnMouseDown;
    property OnMouseMove;
    property OnMouseUp;
    property OnStartDock;
    property OnStartDrag;
  end;

{ TTeThemeDBLookupComboBox }

  TTePopupDataList = class(TTeThemeDBLookupListBox)
  private
    procedure WMMouseActivate(var Message: TMessage); message WM_MOUSEACTIVATE;
  protected
    procedure CreateParams(var Params: TCreateParams); override;
  public
    constructor Create(AOwner: TComponent); override;
  end;

  TDropDownAlign = (daLeft, daRight, daCenter);

  TTeThemeDBLookupComboBox = class(TTeDBLookupControl)
  private
    FDataList: TTePopupDataList;
    FButtonWidth: Integer;
    FText: string;
    FDropDownRows: Integer;
    FDropDownWidth: Integer;
    FDropDownAlign: TDropDownAlign;
    FListVisible: Boolean;
    FPressed: Boolean;
    FTracking: Boolean;
    FAlignment: TAlignment;
    FLookupMode: Boolean;
    FOnDropDown: TNotifyEvent;
    FOnCloseUp: TNotifyEvent;
    procedure ListMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure StopTracking;
    procedure TrackButton(X, Y: Integer);
    procedure CMDialogKey(var Message: TCMDialogKey); message CM_DIALOGKEY;
    procedure CMCancelMode(var Message: TCMCancelMode); message CM_CANCELMODE;
    procedure CMFontChanged(var Message: TMessage); message CM_FONTCHANGED;
    procedure CMGetDataLink(var Message: TMessage); message CM_GETDATALINK;
    procedure WMCancelMode(var Message: TMessage); message WM_CANCELMODE;
    procedure WMKillFocus(var Message: TWMKillFocus); message WM_KILLFOCUS;
    procedure CMBiDiModeChanged(var Message: TMessage); message CM_BIDIMODECHANGED;
  protected
    procedure CreateParams(var Params: TCreateParams); override;

    procedure PaintBuffer; override;

    procedure KeyDown(var Key: Word; Shift: TShiftState); override;
    procedure KeyPress(var Key: Char); override;
    procedure KeyValueChanged; override;
    procedure MouseDown(Button: TMouseButton; Shift: TShiftState;
      X, Y: Integer); override;
    procedure MouseMove(Shift: TShiftState; X, Y: Integer); override;
    procedure MouseUp(Button: TMouseButton; Shift: TShiftState;
      X, Y: Integer); override;
    procedure UpdateListFields; override;
  public
    constructor Create(AOwner: TComponent); override;
    procedure Loaded; override;

    procedure CloseUp(Accept: Boolean); virtual;
    procedure DropDown; virtual;
    function ExecuteAction(Action: TBasicAction): Boolean; override;
    procedure SetBounds(ALeft, ATop, AWidth, AHeight: Integer); override;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    function UseRightToLeftAlignment: Boolean; override;
    property KeyValue;
    property ListVisible: Boolean read FListVisible;
    property Text: string read FText;
  published
    property Anchors;
    property BiDiMode;
    property Color;
    property Constraints;
    property Ctl3D;
    property DataField;
    property DataSource;
    property DragCursor;
    property DragKind;
    property DragMode;
    property DropDownAlign: TDropDownAlign read FDropDownAlign write FDropDownAlign default daLeft;
    property DropDownRows: Integer read FDropDownRows write FDropDownRows default 7;
    property DropDownWidth: Integer read FDropDownWidth write FDropDownWidth default 0;
    property Enabled;
    property Font;
    property ImeMode;
    property ImeName;
    property KeyField;
    property ListField;
    property ListFieldIndex;
    property ListSource;
    property ParentBiDiMode;
    property ParentColor;
    property ParentCtl3D;
    property ParentFont;
    property ParentShowHint;
    property PopupMenu;
    property ReadOnly;
    property ShowHint;
    property TabOrder;
    property TabStop;
    property Visible;
    property OnClick;
    property OnCloseUp: TNotifyEvent read FOnCloseUp write FOnCloseUp;
    property OnDragDrop;
    property OnDragOver;
    property OnDropDown: TNotifyEvent read FOnDropDown write FOnDropDown;
    property OnEndDock;
    property OnEndDrag;
    property OnEnter;
    property OnExit;
    property OnKeyDown;
    property OnKeyPress;
    property OnKeyUp;
    property OnMouseDown;
    property OnMouseMove;
    property OnMouseUp;
    property OnStartDock;
    property OnStartDrag;
  end;

{ TTeThemeDBEditButton }
 
  TTeThemeDBEditButton = class(TTeThemeEditButton)
  private
    FDataLink: TFieldDataLink;
    FDataChanging: boolean;
    procedure ActiveChange(Sender: TObject);
    procedure DataChange(Sender: TObject);
    procedure EditingChange(Sender: TObject);
    function GetDataField: string;
    function GetDataSource: TDataSource;
    function GetField: TField;
    function GetReadOnly: Boolean;
    procedure ResetMaxLength;
    procedure SetDataField(const Value: string);
    procedure SetDataSource(Value: TDataSource);
    procedure SetReadOnly(Value: Boolean);
    procedure UpdateData(Sender: TObject);
    procedure WMCut(var Message: TMessage); message WM_CUT;
    procedure WMPaste(var Message: TMessage); message WM_PASTE;
    procedure WMUndo(var Message: TMessage); message WM_UNDO;
    procedure CMGetDataLink(var Message: TMessage); message CM_GETDATALINK;
  protected
    procedure Change; override;
    procedure KeyDown(var Key: Word; Shift: TShiftState); override;
    procedure KeyPress(var Key: Char); override;
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
    procedure HasFocus; override;
    procedure KillFocus; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;
 
    function ExecuteAction(Action: TBasicAction): Boolean; override;
    function UpdateAction(Action: TBasicAction): Boolean; override;
    function UseRightToLeftAlignment: Boolean; override;
    property Field: TField read GetField;
  published
    property Anchors;
    property AutoSelect;
    property AutoSize;
    property BiDiMode;
    property CharCase;
    property Color;
    property Constraints;
    property Ctl3D;
    property DataField: string read GetDataField write SetDataField;
    property DataSource: TDataSource read GetDataSource write SetDataSource;
    property DragCursor;
    property DragKind;
    property DragMode;
    property Enabled;
    property Font;
    property ImeMode;
    property ImeName;
    property MaxLength;
    property ParentBiDiMode;
    property ParentColor;
    property ParentCtl3D;
    property ParentFont;
    property ParentShowHint;
    property PopupMenu;
    property ReadOnly: Boolean read GetReadOnly write SetReadOnly default False;
    property ShowHint;
    property TabOrder;
    property TabStop;
    property Visible;
    property OnChange;
    property OnClick;
    property OnDblClick;
    property OnDragDrop;
    property OnDragOver;
    property OnEndDock;
    property OnEndDrag;
    property OnEnter;
    property OnExit;
    property OnKeyDown;
    property OnKeyPress;
    property OnKeyUp;
    property OnMouseDown;
    property OnMouseMove;
    property OnMouseUp;
    property OnStartDock;
    property OnStartDrag;
  end;

implementation {===============================================================}

uses te_winxp, Clipbrd, DBConsts, Dialogs, Math {$IFDEF KS_COMPILER6_UP}, VDBConsts, Variants {$ENDIF};

{ TTeThemeDBMaskEdit }

procedure TTeThemeDBMaskEdit.ResetMaxLength;
var
  F: TField;
begin
  if (MaxLength > 0) and Assigned(DataSource) and Assigned(DataSource.DataSet) then
  begin
    F := DataSource.DataSet.FindField(DataField);
    if Assigned(F) and (F.DataType in [ftString, ftWideString]) and (F.Size = MaxLength) then
      MaxLength := 0;
  end;
end;

constructor TTeThemeDBMaskEdit.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  inherited ReadOnly := True;
  ControlStyle := ControlStyle + [csReplicatable];
  FDataLink := TFieldDataLink.Create;
  FDataLink.Control := Self;
  FDataLink.OnDataChange := DataChange;
  FDataLink.OnEditingChange := EditingChange;
  FDataLink.OnUpdateData := UpdateData;
  FDataLink.OnActiveChange := ActiveChange;
end;

destructor TTeThemeDBMaskEdit.Destroy;
begin
  FDataLink.Free;
  FDataLink := nil;
  FCanvas.Free;
  inherited Destroy;
end;

procedure TTeThemeDBMaskEdit.Loaded;
begin
  inherited Loaded;
  ResetMaxLength;
  if (csDesigning in ComponentState) then DataChange(Self);
end;

procedure TTeThemeDBMaskEdit.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited Notification(AComponent, Operation);
  if (Operation = opRemove) and (FDataLink <> nil) and
    (AComponent = DataSource) then DataSource := nil;
end;

function TTeThemeDBMaskEdit.UseRightToLeftAlignment: Boolean;
begin
  Result := DBUseRightToLeftAlignment(Self, Field);
end;

procedure TTeThemeDBMaskEdit.KeyDown(var Key: Word; Shift: TShiftState);
begin
  inherited KeyDown(Key, Shift);
  if (Key = VK_DELETE) or ((Key = VK_INSERT) and (ssShift in Shift)) then
    FDataLink.Edit;
end;

procedure TTeThemeDBMaskEdit.KeyPress(var Key: Char);
begin
  inherited KeyPress(Key);
  if (Key in [#32..#255]) and (FDataLink.Field <> nil) and
    not FDataLink.Field.IsValidChar(Key) then
  begin
    MessageBeep(0);
    Key := #0;
  end;
  case Key of
    ^H, ^V, ^X, #32..#255:
      FDataLink.Edit;
    #27:
      begin
        FDataLink.Reset;
        SelectAll;
        Key := #0;
      end;
  end;
end;

function TTeThemeDBMaskEdit.EditCanModify: Boolean;
begin
  Result := FDataLink.Edit;
end;

procedure TTeThemeDBMaskEdit.Reset;
begin
  FDataLink.Reset;
  SelectAll;
end;

procedure TTeThemeDBMaskEdit.SetFocused(Value: Boolean);
begin
  if FFocused <> Value then
  begin
    FFocused := Value;
    if (FAlignment <> taLeftJustify) and not IsMasked then Invalidate;
    FDataLink.Reset;
  end;
end;

procedure TTeThemeDBMaskEdit.Change;
begin
  FDataLink.Modified;
  inherited Change;
end;

function TTeThemeDBMaskEdit.GetDataSource: TDataSource;
begin
  Result := FDataLink.DataSource;
end;

procedure TTeThemeDBMaskEdit.SetDataSource(Value: TDataSource);
begin
  if not (FDataLink.DataSourceFixed and (csLoading in ComponentState)) then
    FDataLink.DataSource := Value;
  if Value <> nil then Value.FreeNotification(Self);
end;

function TTeThemeDBMaskEdit.GetDataField: string;
begin
  Result := FDataLink.FieldName;
end;

procedure TTeThemeDBMaskEdit.SetDataField(const Value: string);
begin
  if not (csDesigning in ComponentState) then
    ResetMaxLength;
  FDataLink.FieldName := Value;
end;

function TTeThemeDBMaskEdit.GetReadOnly: Boolean;
begin
  Result := FDataLink.ReadOnly;
end;

procedure TTeThemeDBMaskEdit.SetReadOnly(Value: Boolean);
begin
  FDataLink.ReadOnly := Value;
end;

function TTeThemeDBMaskEdit.GetField: TField;
begin
  Result := FDataLink.Field;
end;

procedure TTeThemeDBMaskEdit.ActiveChange(Sender: TObject);
begin
  ResetMaxLength;
end;

procedure TTeThemeDBMaskEdit.DataChange(Sender: TObject);
begin
  if FDataLink.Field <> nil then
  begin
    if FAlignment <> FDataLink.Field.Alignment then
    begin
      EditText := '';  {forces update}
      FAlignment := FDataLink.Field.Alignment;
    end;
    EditMask := FDataLink.Field.EditMask;
    if not (csDesigning in ComponentState) then
    begin
      if (FDataLink.Field.DataType in [ftString, ftWideString]) and (MaxLength = 0) then
        MaxLength := FDataLink.Field.Size;
    end;
    if FFocused and FDataLink.CanModify then
      Text := FDataLink.Field.Text
    else
    begin
      EditText := FDataLink.Field.DisplayText;
      if FDataLink.Editing {and FDataLink.FModified } then
        Modified := True;
    end;
  end else
  begin
    FAlignment := taLeftJustify;
    EditMask := '';
    if csDesigning in ComponentState then
      EditText := Name else
      EditText := '';
  end;
end;

procedure TTeThemeDBMaskEdit.EditingChange(Sender: TObject);
begin
  inherited ReadOnly := not FDataLink.Editing;
end;

procedure TTeThemeDBMaskEdit.UpdateData(Sender: TObject);
begin
  ValidateEdit;
  FDataLink.Field.Text := Text;
end;

procedure TTeThemeDBMaskEdit.WMUndo(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;

procedure TTeThemeDBMaskEdit.WMPaste(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;

procedure TTeThemeDBMaskEdit.WMCut(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;

procedure TTeThemeDBMaskEdit.CMEnter(var Message: TCMEnter);
begin
  SetFocused(True);
  inherited;
  if SysLocale.FarEast and FDataLink.CanModify then
    inherited ReadOnly := False;
end;

procedure TTeThemeDBMaskEdit.CMExit(var Message: TCMExit);
begin
  try
    FDataLink.UpdateRecord;
  except
    SelectAll;
    SetFocus;
    raise;
  end;
  SetFocused(False);
  CheckCursor;
  DoExit;
end;

procedure TTeThemeDBMaskEdit.WMPaint(var Message: TWMPaint);
const
  AlignStyle : array[Boolean, TAlignment] of DWORD =
   ((WS_EX_LEFT, WS_EX_RIGHT, WS_EX_LEFT),
    (WS_EX_RIGHT, WS_EX_LEFT, WS_EX_LEFT));
var
  Left: Integer;
  Margins: TPoint;
  R: TRect;
  DC: HDC;
  PS: TPaintStruct;
  S: string;
  AAlignment: TAlignment;
  ExStyle: DWORD;
begin
  AAlignment := FAlignment;
  if UseRightToLeftAlignment then ChangeBiDiModeAlignment(AAlignment);
  if ((AAlignment = taLeftJustify) or FFocused) {and
    not (csPaintCopy in ControlState) }then
  begin
    if SysLocale.MiddleEast and HandleAllocated and (IsRightToLeft) then
    begin { This keeps the right aligned text, right aligned }
      ExStyle := DWORD(GetWindowLong(Handle, GWL_EXSTYLE)) and (not WS_EX_RIGHT) and
        (not WS_EX_RTLREADING) and (not WS_EX_LEFTSCROLLBAR);
      if UseRightToLeftReading then ExStyle := ExStyle or WS_EX_RTLREADING;
      if UseRightToLeftScrollbar then ExStyle := ExStyle or WS_EX_LEFTSCROLLBAR;
      ExStyle := ExStyle or
        AlignStyle[UseRightToLeftAlignment, AAlignment];
      if DWORD(GetWindowLong(Handle, GWL_EXSTYLE)) <> ExStyle then
        SetWindowLong(Handle, GWL_EXSTYLE, ExStyle);
    end;
    inherited;
    Exit;
  end;
{ Since edit controls do not handle justification unless multi-line (and
  then only poorly) we will draw right and center justify manually unless
  the edit has the focus. }
  if FCanvas = nil then
  begin
    FCanvas := TControlCanvas.Create;
    FCanvas.Control := Self;
  end;
  DC := Message.DC;
  if DC = 0 then DC := BeginPaint(Handle, PS);
  FCanvas.Handle := DC;
  try
    FCanvas.Font := Font;
    with FCanvas do
    begin
      R := ClientRect;
      if not (NewStyleControls and Ctl3D) and (BorderStyle = bsSingle) then
      begin
        Brush.Color := clWindowFrame;
        FrameRect(R);
        InflateRect(R, -1, -1);
      end;
      Brush.Color := Color;
      if not Enabled then
        Font.Color := clGrayText;
      if (csPaintCopy in ControlState) and (FDataLink.Field <> nil) then
      begin
        S := FDataLink.Field.DisplayText;
        case CharCase of
          StdCtrls.ecUpperCase: S := AnsiUpperCase(S);
          StdCtrls.ecLowerCase: S := AnsiLowerCase(S);
        end;
      end else
        S := EditText;
      if PasswordChar <> #0 then FillChar(S[1], Length(S), PasswordChar);
      Margins := GetTextMargins;
      case AAlignment of
        taLeftJustify: Left := Margins.X;
        taRightJustify: Left := ClientWidth - TextWidth(S) - Margins.X - 1;
      else
        Left := (ClientWidth - TextWidth(S)) div 2;
      end;
      if SysLocale.MiddleEast then UpdateTextFlags;
      TextRect(R, Left, Margins.Y, S);
    end;
  finally
    FCanvas.Handle := 0;
    if Message.DC = 0 then EndPaint(Handle, PS);
  end;
end;

procedure TTeThemeDBMaskEdit.CMGetDataLink(var Message: TMessage);
begin
  Message.Result := Integer(FDataLink);
end;

function TTeThemeDBMaskEdit.GetTextMargins: TPoint;
var
  DC: HDC;
  SaveFont: HFont;
  I: Integer;
  SysMetrics, Metrics: TTextMetric;
begin
  if NewStyleControls then
  begin
    if BorderStyle = bsNone then I := 0 else
      if Ctl3D then I := 1 else I := 2;
    Result.X := SendMessage(Handle, EM_GETMARGINS, 0, 0) and $0000FFFF + I;
    Result.Y := I;
  end else
  begin
    if BorderStyle = bsNone then I := 0 else
    begin
      DC := GetDC(0);
      GetTextMetrics(DC, SysMetrics);
      SaveFont := SelectObject(DC, Font.Handle);
      GetTextMetrics(DC, Metrics);
      SelectObject(DC, SaveFont);
      ReleaseDC(0, DC);
      I := SysMetrics.tmHeight;
      if I > Metrics.tmHeight then I := Metrics.tmHeight;
      I := I div 4;
    end;
    Result.X := I;
    Result.Y := I;
  end;
end;

function TTeThemeDBMaskEdit.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (FDataLink <> nil) and
    FDataLink.ExecuteAction(Action);
end;

function TTeThemeDBMaskEdit.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (FDataLink <> nil) and
    FDataLink.UpdateAction(Action);
end;

{ TTeThemeDBMemo }

constructor TTeThemeDBMemo.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  inherited ReadOnly := True;
  ControlStyle := ControlStyle + [csReplicatable];
  FAutoDisplay := True;
  FDataLink := TFieldDataLink.Create;
  FDataLink.Control := Self;
  FDataLink.OnDataChange := DataChange;
  FDataLink.OnEditingChange := EditingChange;
  FDataLink.OnUpdateData := UpdateData;
  FPaintControl := TPaintControl.Create(Self, 'EDIT');
end;

destructor TTeThemeDBMemo.Destroy;
begin
  FPaintControl.Free;
  FDataLink.Free;
  FDataLink := nil;
  inherited Destroy;
end;

procedure TTeThemeDBMemo.Loaded;
begin
  inherited Loaded;
  if (csDesigning in ComponentState) then DataChange(Self);
end;

procedure TTeThemeDBMemo.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited Notification(AComponent, Operation);
  if (Operation = opRemove) and (FDataLink <> nil) and
    (AComponent = DataSource) then DataSource := nil;
end;

function TTeThemeDBMemo.UseRightToLeftAlignment: Boolean;
begin
  Result := DBUseRightToLeftAlignment(Self, Field);
end;

procedure TTeThemeDBMemo.KeyDown(var Key: Word; Shift: TShiftState);
begin
  inherited KeyDown(Key, Shift);
  if FMemoLoaded then
  begin
    if (Key = VK_DELETE) or ((Key = VK_INSERT) and (ssShift in Shift)) then
      FDataLink.Edit;
  end;
end;

procedure TTeThemeDBMemo.KeyPress(var Key: Char);
begin
  inherited KeyPress(Key);
  if FMemoLoaded then
  begin
    if (Key in [#32..#255]) and (FDataLink.Field <> nil) and
      not FDataLink.Field.IsValidChar(Key) then
    begin
      MessageBeep(0);
      Key := #0;
    end;
    case Key of
      ^H, ^I, ^J, ^M, ^V, ^X, #32..#255:
        FDataLink.Edit;
      #27:
        FDataLink.Reset;
    end;
  end else
  begin
    if Key = #13 then LoadMemo;
    Key := #0;
  end;
end;

procedure TTeThemeDBMemo.Change;
begin
  if FMemoLoaded then FDataLink.Modified;
  FMemoLoaded := True;
  inherited Change;
end;

function TTeThemeDBMemo.GetDataSource: TDataSource;
begin
  Result := FDataLink.DataSource;
end;

procedure TTeThemeDBMemo.SetDataSource(Value: TDataSource);
begin
  if not (FDataLink.DataSourceFixed and (csLoading in ComponentState)) then
    FDataLink.DataSource := Value;
  if Value <> nil then Value.FreeNotification(Self);
end;

function TTeThemeDBMemo.GetDataField: string;
begin
  Result := FDataLink.FieldName;
end;

procedure TTeThemeDBMemo.SetDataField(const Value: string);
begin
  FDataLink.FieldName := Value;
end;

function TTeThemeDBMemo.GetReadOnly: Boolean;
begin
  Result := FDataLink.ReadOnly;
end;

procedure TTeThemeDBMemo.SetReadOnly(Value: Boolean);
begin
  FDataLink.ReadOnly := Value;
end;

function TTeThemeDBMemo.GetField: TField;
begin
  Result := FDataLink.Field;
end;

procedure TTeThemeDBMemo.LoadMemo;
begin
  if not FMemoLoaded and Assigned(FDataLink.Field) and FDataLink.Field.IsBlob then
  begin
    try
      Lines.Text := FDataLink.Field.AsString;
      FMemoLoaded := True;
    except
      { Memo too large }
      on E:EInvalidOperation do
        Lines.Text := Format('(%s)', [E.Message]);
    end;
    EditingChange(Self);
  end;
end;

procedure TTeThemeDBMemo.DataChange(Sender: TObject);
begin
  if FDataLink.Field <> nil then
    if FDataLink.Field.IsBlob then
    begin
      if FAutoDisplay or (FDataLink.Editing and FMemoLoaded) then
      begin
        FMemoLoaded := False;
        LoadMemo;
      end else
      begin
        Text := Format('(%s)', [FDataLink.Field.DisplayLabel]);
        FMemoLoaded := False;
      end;
    end else
    begin
      if FFocused and FDataLink.CanModify then
        Text := FDataLink.Field.Text
      else
        Text := FDataLink.Field.DisplayText;
      FMemoLoaded := True;
    end
  else
  begin
    if csDesigning in ComponentState then Text := Name else Text := '';
    FMemoLoaded := False;
  end;
  if HandleAllocated then
    RedrawWindow(Handle, nil, 0, RDW_INVALIDATE or RDW_ERASE or RDW_FRAME);
end;

procedure TTeThemeDBMemo.EditingChange(Sender: TObject);
begin
  inherited ReadOnly := not (FDataLink.Editing and FMemoLoaded);
end;

procedure TTeThemeDBMemo.UpdateData(Sender: TObject);
begin
  FDataLink.Field.AsString := Text;
end;

procedure TTeThemeDBMemo.SetFocused(Value: Boolean);
begin
  if FFocused <> Value then
  begin
    FFocused := Value;
    if not Assigned(FDataLink.Field) or not FDataLink.Field.IsBlob then
      FDataLink.Reset;
  end;
end;

procedure TTeThemeDBMemo.WndProc(var Message: TMessage);
begin
  with Message do
    if (Msg = WM_CREATE) or (Msg = WM_WINDOWPOSCHANGED) or
      (Msg = CM_FONTCHANGED) then FPaintControl.DestroyHandle;
  inherited;
end;

procedure TTeThemeDBMemo.CMEnter(var Message: TCMEnter);
begin
  SetFocused(True);
  inherited;
  if SysLocale.FarEast and FDataLink.CanModify then
    inherited ReadOnly := False;
end;

procedure TTeThemeDBMemo.CMExit(var Message: TCMExit);
begin
  try
    FDataLink.UpdateRecord;
  except
    SetFocus;
    raise;
  end;
  SetFocused(False);
  inherited;
end;

procedure TTeThemeDBMemo.SetAutoDisplay(Value: Boolean);
begin
  if FAutoDisplay <> Value then
  begin
    FAutoDisplay := Value;
    if Value then LoadMemo;
  end;
end;

procedure TTeThemeDBMemo.WMLButtonDblClk(var Message: TWMLButtonDblClk);
begin
  if not FMemoLoaded then LoadMemo else inherited;
end;

procedure TTeThemeDBMemo.WMCut(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;

procedure TTeThemeDBMemo.WMUndo(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;

procedure TTeThemeDBMemo.WMPaste(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;

procedure TTeThemeDBMemo.CMGetDataLink(var Message: TMessage);
begin
  Message.Result := Integer(FDataLink);
end;

function TTeThemeDBMemo.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (FDataLink <> nil) and
    FDataLink.ExecuteAction(Action);
end;

function TTeThemeDBMemo.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (FDataLink <> nil) and
    FDataLink.UpdateAction(Action);
end;

{ TTeThemeDBText }

constructor TTeThemeDBText.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  ControlStyle := ControlStyle + [csReplicatable];
  AutoSize := False;
  ShowAccelChar := False;
  FDataLink := TFieldDataLink.Create;
  FDataLink.Control := Self;
  FDataLink.OnDataChange := DataChange;
end;

destructor TTeThemeDBText.Destroy;
begin
  FDataLink.Free;
  FDataLink := nil;
  inherited Destroy;
end;

procedure TTeThemeDBText.Loaded;
begin
  inherited Loaded;
  if (csDesigning in ComponentState) then DataChange(Self);
end;

procedure TTeThemeDBText.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited Notification(AComponent, Operation);
  if (Operation = opRemove) and (FDataLink <> nil) and
    (AComponent = DataSource) then DataSource := nil;
end;

function TTeThemeDBText.UseRightToLeftAlignment: Boolean;
begin
  if FDataLink = nil then
    Result := false
  else
    Result := DBUseRightToLeftAlignment(Self, Field);
end;

procedure TTeThemeDBText.SetAutoSize(Value: Boolean);
begin
  if AutoSize <> Value then
  begin
    if Value and FDataLink.DataSourceFixed then DatabaseError(SDataSourceFixed);
    inherited SetAutoSize(Value);
  end;
end;

function TTeThemeDBText.GetDataSource: TDataSource;
begin
  Result := FDataLink.DataSource;
end;

procedure TTeThemeDBText.SetDataSource(Value: TDataSource);
begin
  if not (FDataLink.DataSourceFixed and (csLoading in ComponentState)) then
    FDataLink.DataSource := Value;
  if Value <> nil then Value.FreeNotification(Self);
end;

function TTeThemeDBText.GetDataField: string;
begin
  Result := FDataLink.FieldName;
end;

procedure TTeThemeDBText.SetDataField(const Value: string);
begin
  FDataLink.FieldName := Value;
end;

function TTeThemeDBText.GetField: TField;
begin
  Result := FDataLink.Field;
end;

function TTeThemeDBText.GetFieldText: string;
begin
  if FDataLink.Field <> nil then
    Result := FDataLink.Field.DisplayText
  else
    if csDesigning in ComponentState then Result := Name else Result := '';
end;

procedure TTeThemeDBText.DataChange(Sender: TObject);
begin
  Caption := GetFieldText;
end;

function TTeThemeDBText.GetLabelText: WideString;
begin
  if FDataLink = nil then
    Result := ''
  else
    if csPaintCopy in ControlState then
      Result := GetFieldText
    else
      Result := Caption;
end;

procedure TTeThemeDBText.CMGetDataLink(var Message: TMessage);
begin
  Message.Result := Integer(FDataLink);
end;

function TTeThemeDBText.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (FDataLink <> nil) and
    FDataLink.ExecuteAction(Action);
end;

function TTeThemeDBText.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (FDataLink <> nil) and
    FDataLink.UpdateAction(Action);
end;

{ TTeThemeDBEdit }

constructor TTeThemeDBEdit.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  inherited ReadOnly := True;
  FDataLink := TFieldDataLink.Create;
  FDataLink.Control := Self;
  FDataLink.OnDataChange := DataChange;
  FDataLink.OnEditingChange := EditingChange;
  FDataLink.OnUpdateData := UpdateData;
  FDataLink.OnActiveChange := ActiveChange;
end;

destructor TTeThemeDBEdit.Destroy;
begin
  FDataLink.Free;
  FDataLink := nil;
  inherited Destroy;
end;

procedure TTeThemeDBEdit.Loaded;
begin
  inherited Loaded;
  ResetMaxLength;
  if (csDesigning in ComponentState) then DataChange(Self);
end;

procedure TTeThemeDBEdit.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited Notification(AComponent, Operation);
  if (Operation = opRemove) and (FDataLink <> nil) and
    (AComponent = DataSource) then DataSource := nil;
end;

function TTeThemeDBEdit.UseRightToLeftAlignment: Boolean;
begin
  Result := DBUseRightToLeftAlignment(Self, Field);
end;

procedure TTeThemeDBEdit.KeyDown(var Key: Word; Shift: TShiftState);
begin
  inherited KeyDown(Key, Shift);
  if (Key = VK_DELETE) or ((Key = VK_INSERT) and (ssShift in Shift)) then
    FDataLink.Edit;
end;

procedure TTeThemeDBEdit.KeyPress(var Key: Char);
begin
  if (Key in [#32..#255]) and (FDataLink.Field <> nil) and
    not FDataLink.Field.IsValidChar(Key) then
  begin
    MessageBeep(0);
    Key := #0;
  end;
  case Key of
    ^H, ^V, ^X, #32..#255:
      FDataLink.Edit;
    #27:
      begin
        FDataLink.Reset;
        SelectAll;
        Key := #0;
      end;
  end;

  inherited KeyPress(Key);
end;

procedure TTeThemeDBEdit.Change;
begin
  if FDataChanging then
  begin
    inherited Change;
    CaretPosition := 0;
  end
  else
  begin
    FDataLink.Modified;
    inherited Change;
  end;
end;

procedure TTeThemeDBEdit.ActiveChange(Sender: TObject);
begin
  ResetMaxLength;
end;

procedure TTeThemeDBEdit.DataChange(Sender: TObject);
begin
  FDataChanging := true;
  try
    if FDataLink.Field <> nil then
    begin
      if TextAlignment <> FDataLink.Field.Alignment then
      begin
        Text := '';  {forces update}
        TextAlignment := FDataLink.Field.Alignment;
      end;
  //    EditMask := FDataLink.Field.EditMask;
      if not (csDesigning in ComponentState) then
      begin
        if (FDataLink.Field.DataType in [ftString, ftWideString]) and (MaxLength = 0) then
          MaxLength := FDataLink.Field.Size;
      end;
      if Focused and FDataLink.CanModify then
      begin
        Text := FDataLink.Field.Text
      end
      else
      begin
        Text := FDataLink.Field.DisplayText;
  {      if FDataLink.Editing and FDataLink.FModified then
          Modified := True;}
      end;
    end
    else
    begin
      TextAlignment := taLeftJustify;
  //    EditMask := '';
      if csDesigning in ComponentState then
        Text := Name
      else
        Text := '';
    end;
  finally
    FDataChanging := false;
  end;
end;

procedure TTeThemeDBEdit.EditingChange(Sender: TObject);
begin
  inherited ReadOnly := not FDataLink.Editing;
end;

procedure TTeThemeDBEdit.UpdateData(Sender: TObject);
begin
//  ValidateEdit;
  FDataLink.Field.Text := Text;
end;

procedure TTeThemeDBEdit.WMUndo(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;

procedure TTeThemeDBEdit.WMPaste(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;

procedure TTeThemeDBEdit.WMCut(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;

procedure TTeThemeDBEdit.HasFocus;
begin
  inherited;
  if SysLocale.FarEast and FDataLink.CanModify then
    inherited ReadOnly := False;
end;

procedure TTeThemeDBEdit.KillFocus;
begin
  try
    FDataLink.UpdateRecord;
  except
    SelectAll;
    SetFocus;
    raise;
  end;
  inherited ;
end;

procedure TTeThemeDBEdit.CMGetDataLink(var Message: TMessage);
begin
  Message.Result := Integer(FDataLink);
end;

function TTeThemeDBEdit.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (FDataLink <> nil) and
    FDataLink.ExecuteAction(Action);
end;

function TTeThemeDBEdit.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (FDataLink <> nil) and
    FDataLink.UpdateAction(Action);
end;

procedure TTeThemeDBEdit.ResetMaxLength;
var
  F: TField;
begin
  if (MaxLength > 0) and Assigned(DataSource) and Assigned(DataSource.DataSet) then
  begin
    F := DataSource.DataSet.FindField(DataField);
    if Assigned(F) and (F.DataType in [ftString, ftWideString]) and (F.Size = MaxLength) then
      MaxLength := 0;
  end;
end;

function TTeThemeDBEdit.GetDataSource: TDataSource;
begin
  Result := FDataLink.DataSource;
end;

procedure TTeThemeDBEdit.SetDataSource(Value: TDataSource);
begin
  if not (FDataLink.DataSourceFixed and (csLoading in ComponentState)) then
    FDataLink.DataSource := Value;
  if Value <> nil then Value.FreeNotification(Self);
end;

function TTeThemeDBEdit.GetDataField: string;
begin
  Result := FDataLink.FieldName;
end;

procedure TTeThemeDBEdit.SetDataField(const Value: string);
begin
  if not (csDesigning in ComponentState) then
    ResetMaxLength;
  FDataLink.FieldName := Value;
end;

function TTeThemeDBEdit.GetReadOnly: Boolean;
begin
  Result := FDataLink.ReadOnly;
end;

procedure TTeThemeDBEdit.SetReadOnly(Value: Boolean);
begin
  FDataLink.ReadOnly := Value;
end;

function TTeThemeDBEdit.GetField: TField;
begin
  Result := FDataLink.Field;
end;

{ TTeThemeDBCheckBox ==========================================================}

constructor TTeThemeDBCheckBox.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  FValueCheck := STextTrue;
  FValueUncheck := STextFalse;
  FDataLink := TFieldDataLink.Create;
  FDataLink.Control := Self;
  FDataLink.OnDataChange := DataChange;
  FDataLink.OnUpdateData := UpdateData;

//  State := cbUnchecked;
end;

destructor TTeThemeDBCheckBox.Destroy;
begin
  FDataLink.Free;
  FDataLink := nil;
  inherited Destroy;
end;

procedure TTeThemeDBCheckBox.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited Notification(AComponent, Operation);
  if (Operation = opRemove) and (FDataLink <> nil) and
    (AComponent = DataSource) then DataSource := nil;
end;

function TTeThemeDBCheckBox.UseRightToLeftAlignment: Boolean;
begin
  Result := DBUseRightToLeftAlignment(Self, Field);
end;

function TTeThemeDBCheckBox.GetFieldState: TCheckBoxState;
var
  Text: string;
begin
  if FDatalink.Field <> nil then
    if FDataLink.Field.IsNull then
      Result := cbGrayed
    else if FDataLink.Field.DataType = ftBoolean then
      if FDataLink.Field.AsBoolean then
        Result := cbChecked
      else
        Result := cbUnchecked
    else
    begin
      Result := cbGrayed;
      Text := FDataLink.Field.Text;
      if ValueMatch(FValueCheck, Text) then Result := cbChecked else
        if ValueMatch(FValueUncheck, Text) then Result := cbUnchecked;
    end
  else
    Result := cbUnchecked;
end;

procedure TTeThemeDBCheckBox.DataChange(Sender: TObject);
begin
  FDataChanging := true;
  try
    State := GetFieldState;
  finally
    FDataChanging := false;
  end;
end;

procedure TTeThemeDBCheckBox.UpdateData(Sender: TObject);
var
  Pos: Integer;
  S: string;
begin
  if State = cbGrayed then
    FDataLink.Field.Clear
  else
    if FDataLink.Field.DataType = ftBoolean then
      FDataLink.Field.AsBoolean := Checked
    else
    begin
      if Checked then S := FValueCheck else S := FValueUncheck;
      Pos := 1;
      FDataLink.Field.Text := ExtractFieldName(S, Pos);
    end;
end;

function TTeThemeDBCheckBox.ValueMatch(const ValueList, Value: string): Boolean;
var
  Pos: Integer;
begin
  Result := False;
  Pos := 1;
  while Pos <= Length(ValueList) do
    if AnsiCompareText(ExtractFieldName(ValueList, Pos), Value) = 0 then
    begin
      Result := True;
      Break;
    end;
end;

procedure TTeThemeDBCheckBox.Toggle;
begin
  if FDataChanging then inherited Toggle
  else
    if FDataLink.Edit then
    begin
      inherited Toggle;
      FDataLink.Modified;
    end;
end;

procedure TTeThemeDBCheckBox.Click;
begin
  if FDataChanging then inherited Click
  else
    if FDataLink.Edit then
    begin
      inherited Click;
      FDataLink.Modified;
    end;
end;

function TTeThemeDBCheckBox.GetDataSource: TDataSource;
begin
  Result := FDataLink.DataSource;
end;

procedure TTeThemeDBCheckBox.SetDataSource(Value: TDataSource);
begin
  if not (FDataLink.DataSourceFixed and (csLoading in ComponentState)) then
    FDataLink.DataSource := Value;
  if Value <> nil then Value.FreeNotification(Self);
end;

function TTeThemeDBCheckBox.GetDataField: string;
begin
  Result := FDataLink.FieldName;
end;

procedure TTeThemeDBCheckBox.SetDataField(const Value: string);
begin
  FDataLink.FieldName := Value;
end;

function TTeThemeDBCheckBox.GetReadOnly: Boolean;
begin
  Result := FDataLink.ReadOnly;
end;

procedure TTeThemeDBCheckBox.SetReadOnly(Value: Boolean);
begin
  FDataLink.ReadOnly := Value;
end;

function TTeThemeDBCheckBox.GetField: TField;
begin
  Result := FDataLink.Field;
end;

procedure TTeThemeDBCheckBox.KeyPress(var Key: Char);
begin
  inherited KeyPress(Key);
  case Key of
    #8, ' ':
      FDataLink.Edit;
    #27:
      FDataLink.Reset;
  end;
end;

procedure TTeThemeDBCheckBox.SetValueCheck(const Value: string);
begin
  FValueCheck := Value;
  DataChange(Self);
end;

procedure TTeThemeDBCheckBox.SetValueUncheck(const Value: string);
begin
  FValueUncheck := Value;
  DataChange(Self);
end;

procedure TTeThemeDBCheckBox.KillFocus;
begin
  try
    FDataLink.UpdateRecord;
  except
    SetFocus;
    raise;
  end;
  inherited ;
end;

procedure TTeThemeDBCheckBox.CMGetDataLink(var Message: TMessage);
begin
  Message.Result := Integer(FDataLink);
end;

function TTeThemeDBCheckBox.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (FDataLink <> nil) and
    FDataLink.ExecuteAction(Action);
end;

function TTeThemeDBCheckBox.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (FDataLink <> nil) and
    FDataLink.UpdateAction(Action);
end;

{ TTeThemeDBComboBox ================================================================}

constructor TTeThemeDBComboBox.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  ControlStyle := ControlStyle + [csReplicatable];
  FDataLink := TFieldDataLink.Create;
  FDataLink.Control := Self;
  FDataLink.OnDataChange := DataChange;
  FDataLink.OnUpdateData := UpdateData;
  FDataLink.OnEditingChange := EditingChange;
  FPaintControl := TPaintControl.Create(Self, 'COMBOBOX');
end;

destructor TTeThemeDBComboBox.Destroy;
begin
  FPaintControl.Free;
  FDataLink.Free;
  FDataLink := nil;
  inherited Destroy;
end;

procedure TTeThemeDBComboBox.Loaded;
begin
  inherited Loaded;
  if (csDesigning in ComponentState) then DataChange(Self);
end;

procedure TTeThemeDBComboBox.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited Notification(AComponent, Operation);
  if (Operation = opRemove) and (FDataLink <> nil) and
    (AComponent = DataSource) then DataSource := nil;
end;

procedure TTeThemeDBComboBox.CreateWnd;
begin
  inherited CreateWnd;
  SetEditReadOnly;
end;

procedure TTeThemeDBComboBox.DataChange(Sender: TObject);
begin
  FDataChanging := true;
  try
    if FDataLink.Field <> nil then
      SetComboText(FDataLink.Field.Text)
    else
      if csDesigning in ComponentState then
        SetComboText(Name)
      else
        SetComboText('');
  finally
    FDataChanging := false;
  end;
end;

procedure TTeThemeDBComboBox.UpdateData(Sender: TObject);
begin
  FDataLink.Field.Text := GetComboText;
end;

procedure TTeThemeDBComboBox.SetComboText(const Value: string);
var
  I: Integer;
  Redraw: Boolean;
begin
  if Value <> GetComboText then
  begin
    if ComboStyle <> kcsDropDown then
    begin
      try
        if Value = '' then I := -1 else I := Items.IndexOf(Value);
        ItemIndex := I;
      finally
        Invalidate;
      end;
      if I >= 0 then Exit;
    end;
    if ComboStyle = kcsDropDown then Text := Value;
  end;
end;

function TTeThemeDBComboBox.GetComboText: string;
var
  I: Integer;
begin
  if ComboStyle = kcsDropDown then
    Result := Text
  else
  begin
    I := ItemIndex;
    if I < 0 then Result := '' else Result := Items[I];
  end;
end;

procedure TTeThemeDBComboBox.Change;
begin
  if FDataChanging then inherited Change
  else
  begin
    FDataLink.Edit;
    inherited Change;
    FDataLink.Modified;
  end;
end;

procedure TTeThemeDBComboBox.Click;
begin
  if FDataChanging then inherited Change
  else
  begin
    FDataLink.Edit;
    inherited Click;
    FDataLink.Modified;
  end;
end;

function TTeThemeDBComboBox.GetDataSource: TDataSource;
begin
  Result := FDataLink.DataSource;
end;

procedure TTeThemeDBComboBox.SetDataSource(Value: TDataSource);
begin
  if not (FDataLink.DataSourceFixed and (csLoading in ComponentState)) then
    FDataLink.DataSource := Value;
  if Value <> nil then Value.FreeNotification(Self);
end;

function TTeThemeDBComboBox.GetDataField: string;
begin
  Result := FDataLink.FieldName;
end;

procedure TTeThemeDBComboBox.SetDataField(const Value: string);
begin
  FDataLink.FieldName := Value;
end;

function TTeThemeDBComboBox.GetReadOnly: Boolean;
begin
  Result := FDataLink.ReadOnly;
end;

procedure TTeThemeDBComboBox.SetReadOnly(Value: Boolean);
begin
  FDataLink.ReadOnly := Value;
end;

function TTeThemeDBComboBox.GetField: TField;
begin
  Result := FDataLink.Field;
end;

procedure TTeThemeDBComboBox.KeyDown(var Key: Word; Shift: TShiftState);
begin
  inherited KeyDown(Key, Shift);
  if Key in [VK_BACK, VK_DELETE, VK_UP, VK_DOWN, 32..255] then
  begin
    if not FDataLink.Edit and (Key in [VK_UP, VK_DOWN]) then
      Key := 0;
  end;
end;

procedure TTeThemeDBComboBox.KeyPress(var Key: Char);
begin
  inherited KeyPress(Key);
  if (Key in [#32..#255]) and (FDataLink.Field <> nil) and
    not FDataLink.Field.IsValidChar(Key) then
  begin
    MessageBeep(0);
    Key := #0;
  end;
  case Key of
    ^H, ^V, ^X, #32..#255:
      FDataLink.Edit;
    #27:
      begin
        FDataLink.Reset;
        SelectAll;
      end;
  end;
end;

procedure TTeThemeDBComboBox.EditingChange(Sender: TObject);
begin
  SetEditReadOnly;
end;

procedure TTeThemeDBComboBox.SetEditReadOnly;
begin
  if (ComboStyle = kcsDropDown) and HandleAllocated then
    inherited ReadOnly := not FDataLink.Editing;
end;

procedure TTeThemeDBComboBox.HasFocus;
begin
  inherited;
  if SysLocale.FarEast and FDataLink.CanModify then
    inherited ReadOnly := false;
end;

procedure TTeThemeDBComboBox.KillFocus;
begin
  try
    FDataLink.UpdateRecord;
  except
    SelectAll;
    SetFocus;
    raise;
  end;
  inherited;
end;

procedure TTeThemeDBComboBox.SetItems(Value: TStrings);
begin
  Items.Assign(Value);
  DataChange(Self);
end;

function TTeThemeDBComboBox.UseRightToLeftAlignment: Boolean;
begin
  Result := DBUseRightToLeftAlignment(Self, Field);
end;

procedure TTeThemeDBComboBox.CMGetDatalink(var Message: TMessage);
begin
  Message.Result := Integer(FDataLink);
end;

function TTeThemeDBComboBox.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (FDataLink <> nil) and
    FDataLink.ExecuteAction(Action);
end;

function TTeThemeDBComboBox.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (FDataLink <> nil) and
    FDataLink.UpdateAction(Action);
end;

{ TTeThemeDBListBox }

constructor TTeThemeDBListBox.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  FDataLink := TFieldDataLink.Create;
  FDataLink.Control := Self;
  FDataLink.OnDataChange := DataChange;
  FDataLink.OnUpdateData := UpdateData;
end;

destructor TTeThemeDBListBox.Destroy;
begin
  FDataLink.Free;
  FDataLink := nil;
  inherited Destroy;
end;

procedure TTeThemeDBListBox.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited Notification(AComponent, Operation);
  if (Operation = opRemove) and (FDataLink <> nil) and
    (AComponent = DataSource) then DataSource := nil;
end;

function TTeThemeDBListBox.UseRightToLeftAlignment: Boolean;
begin
  Result := DBUseRightToLeftAlignment(Self, Field);
end;

procedure TTeThemeDBListBox.DataChange(Sender: TObject);
begin
  FDataChanging := true;
  try
    if FDataLink.Field <> nil then
      ItemIndex := Items.IndexOf(FDataLink.Field.Text) else
      ItemIndex := -1;
  finally
    FDataChanging := false;
  end;
end;

procedure TTeThemeDBListBox.UpdateData(Sender: TObject);
begin
  if ItemIndex >= 0 then
    FDataLink.Field.Text := Items[ItemIndex] else
    FDataLink.Field.Text := '';
end;

procedure TTeThemeDBListBox.Click;
begin
  if FDataChanging then inherited Click
  else
    if FDataLink.Edit then
    begin
      inherited Click;
      FDataLink.Modified;
    end;
end;

function TTeThemeDBListBox.GetDataSource: TDataSource;
begin
  Result := FDataLink.DataSource;
end;

procedure TTeThemeDBListBox.SetDataSource(Value: TDataSource);
begin
  FDataLink.DataSource := Value;
  if Value <> nil then Value.FreeNotification(Self);
end;

function TTeThemeDBListBox.GetDataField: string;
begin
  Result := FDataLink.FieldName;
end;

procedure TTeThemeDBListBox.SetDataField(const Value: string);
begin
  FDataLink.FieldName := Value;
end;

function TTeThemeDBListBox.GetReadOnly: Boolean;
begin
  Result := FDataLink.ReadOnly;
end;

procedure TTeThemeDBListBox.SetReadOnly(Value: Boolean);
begin
  FDataLink.ReadOnly := Value;
end;

function TTeThemeDBListBox.GetField: TField;
begin
  Result := FDataLink.Field;
end;

procedure TTeThemeDBListBox.KeyDown(var Key: Word; Shift: TShiftState);
begin
  inherited KeyDown(Key, Shift);
  if Key in [VK_PRIOR, VK_NEXT, VK_END, VK_HOME, VK_LEFT, VK_UP,
    VK_RIGHT, VK_DOWN] then
    if not FDataLink.Edit then Key := 0;
end;

procedure TTeThemeDBListBox.KeyPress(var Key: Char);
begin
  inherited KeyPress(Key);
  case Key of
    #32..#255:
      if not FDataLink.Edit then Key := #0;
    #27:
      FDataLink.Reset;
  end;
end;

procedure TTeThemeDBListBox.WMLButtonDown(var Message: TWMLButtonDown);
begin
  if FDataLink.Edit then inherited
  else
  begin
    SetFocus;
    with Message do
      MouseDown(mbLeft, KeysToShiftState(Keys), XPos, YPos);
  end;
end;

procedure TTeThemeDBListBox.KillFocus;
begin
  try
    FDataLink.UpdateRecord;
  except
    SetFocus;
    raise;
  end;
  inherited;
end;

procedure TTeThemeDBListBox.SetItems(Value: TStrings);
begin
  Items.Assign(Value);
  DataChange(Self);
end;

function TTeThemeDBListBox.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (FDataLink <> nil) and
    FDataLink.ExecuteAction(Action);
end;

function TTeThemeDBListBox.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (FDataLink <> nil) and
    FDataLink.UpdateAction(Action);
end;

{ TTeThemeDBRadioGroup }

constructor TTeThemeDBRadioGroup.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  FDataLink := TFieldDataLink.Create;
  FDataLink.Control := Self;
  FDataLink.OnDataChange := DataChange;
  FDataLink.OnUpdateData := UpdateData;
  FValues := TStringList.Create;
end;

destructor TTeThemeDBRadioGroup.Destroy;
begin
  FDataLink.Free;
  FDataLink := nil;
  FValues.Free;
  inherited Destroy;
end;

procedure TTeThemeDBRadioGroup.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited Notification(AComponent, Operation);
  if (Operation = opRemove) and (FDataLink <> nil) and
    (AComponent = DataSource) then DataSource := nil;
end;

function TTeThemeDBRadioGroup.UseRightToLeftAlignment: Boolean;
begin
  Result := inherited UseRightToLeftAlignment;
end;

procedure TTeThemeDBRadioGroup.DataChange(Sender: TObject);
begin
  if FDataLink.Field <> nil then
    Value := FDataLink.Field.Text else
    Value := '';
end;

procedure TTeThemeDBRadioGroup.UpdateData(Sender: TObject);
begin
  if FDataLink.Field <> nil then FDataLink.Field.Text := Value;
end;

function TTeThemeDBRadioGroup.GetDataSource: TDataSource;
begin
  Result := FDataLink.DataSource;
end;

procedure TTeThemeDBRadioGroup.SetDataSource(Value: TDataSource);
begin
  FDataLink.DataSource := Value;
  if Value <> nil then Value.FreeNotification(Self);
end;

function TTeThemeDBRadioGroup.GetDataField: string;
begin
  Result := FDataLink.FieldName;
end;

procedure TTeThemeDBRadioGroup.SetDataField(const Value: string);
begin
  FDataLink.FieldName := Value;
end;

function TTeThemeDBRadioGroup.GetReadOnly: Boolean;
begin
  Result := FDataLink.ReadOnly;
end;

procedure TTeThemeDBRadioGroup.SetReadOnly(Value: Boolean);
begin
  FDataLink.ReadOnly := Value;
end;

function TTeThemeDBRadioGroup.GetField: TField;
begin
  Result := FDataLink.Field;
end;

function TTeThemeDBRadioGroup.GetButtonValue(Index: Integer): string;
begin
  if (Index < FValues.Count) and (FValues[Index] <> '') then
    Result := FValues[Index]
  else if Index < Items.Count then
    Result := Items[Index]
  else
    Result := '';
end;

procedure TTeThemeDBRadioGroup.SetValue(const Value: string);
var
  I, Index: Integer;
begin
  if FValue <> Value then
  begin
    FInSetValue := True;
    try
      Index := -1;
      for I := 0 to Items.Count - 1 do
        if Value = GetButtonValue(I) then
        begin
          Index := I;
          Break;
        end;
      ItemIndex := Index;
    finally
      FInSetValue := False;
    end;
    FValue := Value;
    Change;
  end;
end;

procedure TTeThemeDBRadioGroup.KillFocus;
begin
  try
    FDataLink.UpdateRecord;
  except
    if ItemIndex >= 0 then
      TRadioButton(Controls[ItemIndex]).SetFocus else
      TRadioButton(Controls[0]).SetFocus;
    raise;
  end;
  inherited;
end;

procedure TTeThemeDBRadioGroup.Click;
begin
  if not FInSetValue then
  begin
    inherited Click;
    if ItemIndex >= 0 then Value := GetButtonValue(ItemIndex);
    if FDataLink.Editing then FDataLink.Modified;
  end;
end;

procedure TTeThemeDBRadioGroup.SetItems(Value: TStrings);
begin
  Items.Assign(Value);
  DataChange(Self);
end;

procedure TTeThemeDBRadioGroup.SetValues(Value: TStrings);
begin
  FValues.Assign(Value);
  DataChange(Self);
end;

procedure TTeThemeDBRadioGroup.Change;
begin
  if Assigned(FOnChange) then FOnChange(Self);
end;

procedure TTeThemeDBRadioGroup.KeyPress(var Key: Char);
begin
  inherited KeyPress(Key);
  case Key of
    #8, ' ': FDataLink.Edit;
    #27: FDataLink.Reset;
  end;
end;

function TTeThemeDBRadioGroup.CanModify: Boolean;
begin
  Result := FDataLink.Edit;
end;

function TTeThemeDBRadioGroup.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (DataLink <> nil) and
    DataLink.ExecuteAction(Action);
end;

function TTeThemeDBRadioGroup.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (DataLink <> nil) and
    DataLink.UpdateAction(Action);
end;

{ TTeDataSourceLink }

constructor TTeDataSourceLink.Create;
begin
  inherited Create;
  VisualControl := True;
end;

procedure TTeDataSourceLink.ActiveChanged;
begin
  if FDBLookupControl <> nil then FDBLookupControl.UpdateDataFields;
end;

procedure TTeDataSourceLink.FocusControl(Field: TFieldRef);
begin
  if (Field^ <> nil) and (Field^ = FDBLookupControl.Field) and
    (FDBLookupControl <> nil) and FDBLookupControl.CanFocus then
  begin
    Field^ := nil;
    FDBLookupControl.SetFocus;
  end;
end;

procedure TTeDataSourceLink.LayoutChanged;
begin
  if FDBLookupControl <> nil then FDBLookupControl.UpdateDataFields;
end;

procedure TTeDataSourceLink.RecordChanged(Field: TField);
begin
  if FDBLookupControl <> nil then FDBLookupControl.DataLinkRecordChanged(Field);
end;

{ TTeListSourceLink }

constructor TTeListSourceLink.Create;
begin
  inherited Create;
  VisualControl := True;
end;

procedure TTeListSourceLink.ActiveChanged;
begin
  if FDBLookupControl <> nil then FDBLookupControl.UpdateListFields;
end;

procedure TTeListSourceLink.DataSetChanged;
begin
  if FDBLookupControl <> nil then FDBLookupControl.ListLinkDataChanged;
end;

procedure TTeListSourceLink.LayoutChanged;
begin
  if FDBLookupControl <> nil then FDBLookupControl.UpdateListFields;
end;

{ TTeDBLookupControl }

function VarEquals(const V1, V2: Variant): Boolean;
begin
  Result := False;
  try
    Result := V1 = V2;
  except
  end;
end;

var
  SearchTickCount: Integer = 0;

constructor TTeDBLookupControl.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  ControlStyle := [csOpaque];
  Color := clWindow;
  ParentColor := False;
  TabStop := True;
  FLookupSource := TDataSource.Create(Self);
  FDataLink := TTeDataSourceLink.Create;
  FDataLink.FDBLookupControl := Self;
  FListLink := TTeListSourceLink.Create;
  FListLink.FDBLookupControl := Self;
  FListFields := TList.Create;
  FKeyValue := Null;
end;

destructor TTeDBLookupControl.Destroy;
begin
  inherited Destroy;
  FListFields.Free;
  FListFields := nil;
  FListLink.FDBLookupControl := nil;
  FListLink.Free;
  FListLink := nil;
  FDataLink.FDBLookupControl := nil;
  FDataLink.Free;
  FDataLink := nil;
end;

procedure TTeDBLookupControl.Loaded;
begin
  inherited;
  SetBounds(Left, Top, Width, Height);
end;

function TTeDBLookupControl.CanModify: Boolean;
begin
  Result := FListActive and not ReadOnly and ((FDataLink.DataSource = nil) or
    (FMasterField <> nil) and FMasterField.CanModify);
end;

procedure TTeDBLookupControl.CheckNotCircular;
begin
  if FListLink.Active and FListLink.DataSet.IsLinkedTo(DataSource) then
    DatabaseError(SCircularDataLink);
end;

procedure TTeDBLookupControl.CheckNotLookup;
begin
  if FLookupMode then DatabaseError(SPropDefByLookup);
  if FDataLink.DataSourceFixed then DatabaseError(SDataSourceFixed);
end;

procedure TTeDBLookupControl.UpdateDataFields;
begin
  FDataField := nil;
  FMasterField := nil;
  if FDataLink.Active and (FDataFieldName <> '') then
  begin
    CheckNotCircular;
    FDataField := GetFieldProperty(FDataLink.DataSet, Self, FDataFieldName);
    if FDataField.FieldKind = fkLookup then
      FMasterField := GetFieldProperty(FDataLink.DataSet, Self, FDataField.KeyFields)
    else
      FMasterField := FDataField;
  end;
  SetLookupMode((FDataField <> nil) and (FDataField.FieldKind = fkLookup));
  DataLinkRecordChanged(nil);
end;

procedure TTeDBLookupControl.DataLinkRecordChanged(Field: TField);
begin
  if (Field = nil) or (Field = FMasterField) then
    if FMasterField <> nil then
      SetKeyValue(FMasterField.Value) else
      SetKeyValue(Null);
end;

function TTeDBLookupControl.GetBorderSize: Integer;
var
  Params: TCreateParams;
  R: TRect;
begin
  CreateParams(Params);
  SetRect(R, 0, 0, 0, 0);
  AdjustWindowRectEx(R, Params.Style, False, Params.ExStyle);
  Result := R.Bottom - R.Top;
end;

function TTeDBLookupControl.GetDataSource: TDataSource;
begin
  Result := FDataLink.DataSource;
end;

function TTeDBLookupControl.GetKeyFieldName: string;
begin
  if FLookupMode then Result := '' else Result := FKeyFieldName;
end;

function TTeDBLookupControl.GetListSource: TDataSource;
begin
  if FLookupMode then Result := nil else Result := FListLink.DataSource;
end;

function TTeDBLookupControl.GetReadOnly: Boolean;
begin
  Result := FDataLink.ReadOnly;
end;

function TTeDBLookupControl.GetTextHeight: Integer;
var
  DC: HDC;
  SaveFont: HFont;
  Metrics: TTextMetric;
begin
  DC := GetDC(0);
  SaveFont := SelectObject(DC, Font.Handle);
  GetTextMetrics(DC, Metrics);
  SelectObject(DC, SaveFont);
  ReleaseDC(0, DC);
  Result := Metrics.tmHeight;
end;

procedure TTeDBLookupControl.KeyValueChanged;
begin
end;

procedure TTeDBLookupControl.UpdateListFields;
var
  DataSet: TDataSet;
  ResultField: TField;
begin
  FListActive := False;
  FKeyField := nil;
  FListField := nil;
  FListFields.Clear;
  if FListLink.Active and (FKeyFieldName <> '') then
  begin
    CheckNotCircular;
    DataSet := FListLink.DataSet;
    FKeyField := GetFieldProperty(DataSet, Self, FKeyFieldName);
    try
      DataSet.GetFieldList(FListFields, FListFieldName);
    except
      DatabaseErrorFmt(SFieldNotFound, [Self.Name, FListFieldName]);
    end;
    if FLookupMode then
    begin
      ResultField := GetFieldProperty(DataSet, Self, FDataField.LookupResultField);
      if FListFields.IndexOf(ResultField) < 0 then
        FListFields.Insert(0, ResultField);
      FListField := ResultField;
    end else
    begin
      if FListFields.Count = 0 then FListFields.Add(FKeyField);
      if (FListFieldIndex >= 0) and (FListFieldIndex < FListFields.Count) then
        FListField := FListFields[FListFieldIndex] else
        FListField := FListFields[0];
    end;
    FListActive := True;
  end;
end;

procedure TTeDBLookupControl.ListLinkDataChanged;
begin
end;

function TTeDBLookupControl.LocateKey: Boolean;
var
  KeySave: Variant;
begin
  Result := False;
  try
    KeySave := FKeyValue;
    if not VarIsNull(FKeyValue) and FListLink.DataSet.Active and
      FListLink.DataSet.Locate(FKeyFieldName, FKeyValue, []) then
    begin
      Result := True;
      FKeyValue := KeySave;
    end;
  except
  end;
end;

procedure TTeDBLookupControl.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited Notification(AComponent, Operation);
  if Operation = opRemove then
  begin
    if (FDataLink <> nil) and (AComponent = DataSource) then DataSource := nil;
    if (FListLink <> nil) and (AComponent = ListSource) then ListSource := nil;
  end;
end;

procedure TTeDBLookupControl.ProcessSearchKey(Key: Char);
var
  TickCount: Integer;
  S: string;
  CharMsg: TMsg;
begin
  if (FListField <> nil) and (FListField.FieldKind in [fkData, fkInternalCalc]) and
    (FListField.DataType in [ftString, ftWideString]) then
    case Key of
      #8, #27: SearchText := '';
      #32..#255:
        if CanModify then
        begin
          TickCount := GetTickCount;
          if TickCount - SearchTickCount > 2000 then SearchText := '';
          SearchTickCount := TickCount;
          if SysLocale.FarEast and (Key in LeadBytes) then
            if PeekMessage(CharMsg, Handle, WM_CHAR, WM_CHAR, PM_REMOVE) then
            begin
              if CharMsg.Message = WM_Quit then
              begin
                PostQuitMessage(CharMsg.wparam);
                Exit;
              end;
              SearchText := SearchText + Key;
              Key := Char(CharMsg.wParam);
            end;
          if Length(SearchText) < 32 then
          begin
            S := SearchText + Key;
            try
              if FListLink.DataSet.Locate(FListField.FieldName, S,
                [loCaseInsensitive, loPartialKey]) then
              begin
                SelectKeyValue(FKeyField.Value);
                SearchText := S;
              end;
            except
              { If you attempt to search for a string larger than what the field
                can hold, and exception will be raised.  Just trap it and
                reset the SearchText back to the old value. }
              SearchText := S;
            end;
          end;
        end;
    end;
end;

procedure TTeDBLookupControl.SelectKeyValue(const Value: Variant);
begin
  if FMasterField <> nil then
  begin
    if FDataLink.Edit then
      FMasterField.Value := Value;
  end else
    SetKeyValue(Value);
  Repaint;
  Click;
end;

procedure TTeDBLookupControl.SetDataFieldName(const Value: string);
begin
  if FDataFieldName <> Value then
  begin
    FDataFieldName := Value;
    UpdateDataFields;
  end;
end;

procedure TTeDBLookupControl.SetDataSource(Value: TDataSource);
begin
  if not (FDataLink.DataSourceFixed and (csLoading in ComponentState)) then
    FDataLink.DataSource := Value;
  if Value <> nil then Value.FreeNotification(Self);
end;

procedure TTeDBLookupControl.SetKeyFieldName(const Value: string);
begin
  CheckNotLookup;
  if FKeyFieldName <> Value then
  begin
    FKeyFieldName := Value;
    UpdateListFields;
  end;
end;

procedure TTeDBLookupControl.SetKeyValue(const Value: Variant);
begin
  if not VarEquals(FKeyValue, Value) then
  begin
    FKeyValue := Value;
    KeyValueChanged;
  end;
end;

procedure TTeDBLookupControl.SetListFieldName(const Value: string);
begin
  if FListFieldName <> Value then
  begin
    FListFieldName := Value;
    UpdateListFields;
  end;
end;

procedure TTeDBLookupControl.SetListSource(Value: TDataSource);
begin
  CheckNotLookup;
  FListLink.DataSource := Value;
  if Value <> nil then Value.FreeNotification(Self);
end;

procedure TTeDBLookupControl.SetLookupMode(Value: Boolean);
begin
  if FLookupMode <> Value then
    if Value then
    begin
      FMasterField := GetFieldProperty(FDataField.DataSet, Self, FDataField.KeyFields);
      FLookupSource.DataSet := FDataField.LookupDataSet;
      FKeyFieldName := FDataField.LookupKeyFields;
      FLookupMode := True;
      FListLink.DataSource := FLookupSource;
    end else
    begin
      FListLink.DataSource := nil;
      FLookupMode := False;
      FKeyFieldName := '';
      FLookupSource.DataSet := nil;
      FMasterField := FDataField;
    end;
end;

procedure TTeDBLookupControl.SetReadOnly(Value: Boolean);
begin
  FDataLink.ReadOnly := Value;
end;

procedure TTeDBLookupControl.WMGetDlgCode(var Message: TMessage);
begin
  Message.Result := DLGC_WANTARROWS or DLGC_WANTCHARS;
end;

procedure TTeDBLookupControl.WMKillFocus(var Message: TMessage);
begin
  FHasFocus := False;
  inherited;
  Invalidate;
end;

procedure TTeDBLookupControl.WMSetFocus(var Message: TMessage);
begin
  SearchText := '';
  FHasFocus := True;
  inherited;
  Invalidate;
end;

procedure TTeDBLookupControl.CMEnabledChanged(var Message: TMessage);
begin
  inherited;
  Invalidate;
end;

function TTeDBLookupControl.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (FDataLink <> nil) and
    FDataLink.ExecuteAction(Action);
end;

function TTeDBLookupControl.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (FDataLink <> nil) and
    FDataLink.UpdateAction(Action);
end;

function TTeDBLookupControl.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeDBLookupControl.PaintBorder;
var
  State: TTeThemeButtonState;
begin
  if UseTheme then
  begin
    if not Enabled then
      State := ngsDisabled
    else
      State := ngsNormal;

    FThemeEngine.Theme.DrawControlFrame(Canvas, Classes.Rect(0, 0, FWidth, FHeight), State);
  end
  else
    inherited ;
end;

function TTeDBLookupControl.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeDBLookupControl.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  if HScrollBar <> nil then
    TTeThemeScrollBar(HScrollBar).ThemeEngine := Value;
  if VScrollBar <> nil then
    TTeThemeScrollBar(VScrollBar).ThemeEngine := Value;

  if (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
     (FThemeEngine.Theme.UseTheme)
  then
    Color := FThemeEngine.Theme.GetColor(ngcWindow);

  Invalidate;
end;

procedure TTeDBLookupControl.SetVersion(const Value: TTeThemeVersion);
begin

end;

procedure TTeDBLookupControl.KeyDown(var Key: Word; Shift: TShiftState);
begin
  inherited;
  if (ShortCut(Key, Shift) <> 0) and (FClearKey = ShortCut(Key, Shift)) then                                  //FR
    SelectKeyValue(Null);                                                     
end;

{ TTeThemeDBLookupListBox }

constructor TTeThemeDBLookupListBox.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  BevelKind := kbkSingle;
  BevelWidth := 1;
  BorderWidth := 0;

  ControlStyle := ControlStyle + [csDoubleClicks];
  Width := 121;
  RowCount := 7;
  ScrollBars := ssVertical;
end;

procedure TTeThemeDBLookupListBox.CreateParams(var Params: TCreateParams);
begin
  inherited CreateParams(Params);
end;

procedure TTeThemeDBLookupListBox.CreateWnd;
begin
  inherited CreateWnd;
  UpdateScrollBar;
end;

function TTeThemeDBLookupListBox.GetKeyIndex: Integer;
var
  FieldValue: Variant;
begin
  if not VarIsNull(FKeyValue) then
    for Result := 0 to FRecordCount - 1 do
    begin
      ListLink.ActiveRecord := Result;
      FieldValue := FKeyField.Value;
      ListLink.ActiveRecord := FRecordIndex;
      if VarEquals(FieldValue, FKeyValue) then Exit;
    end;
  Result := -1;
end;

procedure TTeThemeDBLookupListBox.KeyDown(var Key: Word; Shift: TShiftState);
var
  Delta, KeyIndex: Integer;
begin
  inherited KeyDown(Key, Shift);
  if CanModify then
  begin
    Delta := 0;
    case Key of
      VK_UP, VK_LEFT: Delta := -1;
      VK_DOWN, VK_RIGHT: Delta := 1;
      VK_PRIOR: Delta := 1 - FRowCount;
      VK_NEXT: Delta := FRowCount - 1;
      VK_HOME: Delta := -Maxint;
      VK_END: Delta := Maxint;
    end;
    if Delta <> 0 then
    begin
      SearchText := '';
      if Delta = -Maxint then ListLink.DataSet.First else
        if Delta = Maxint then ListLink.DataSet.Last else
        begin
          KeyIndex := GetKeyIndex;
          if KeyIndex >= 0 then
            ListLink.DataSet.MoveBy(KeyIndex - FRecordIndex)
          else
          begin
            KeyValueChanged;
            Delta := 0;
          end;
          ListLink.DataSet.MoveBy(Delta);
        end;
      SelectCurrent;
    end;
  end;
end;

procedure TTeThemeDBLookupListBox.KeyPress(var Key: Char);
begin
  inherited KeyPress(Key);
  ProcessSearchKey(Key);
end;

procedure TTeThemeDBLookupListBox.KeyValueChanged;
begin
  if ListActive and not FLockPosition then
    if not LocateKey then ListLink.DataSet.First;
  if FListField <> nil then
    FSelectedItem := FListField.DisplayText else
    FSelectedItem := '';
end;

procedure TTeThemeDBLookupListBox.UpdateListFields;
begin
  try
    inherited;
  finally
    if ListActive then KeyValueChanged else ListLinkDataChanged;
  end;
end;

procedure TTeThemeDBLookupListBox.ListLinkDataChanged;
begin
  if ListActive then
  begin
    FRecordIndex := ListLink.ActiveRecord;
    FRecordCount := ListLink.RecordCount;
    FKeySelected := not VarIsNull(FKeyValue) or
      not ListLink.DataSet.BOF;
  end else
  begin
    FRecordIndex := 0;
    FRecordCount := 0;
    FKeySelected := False;
  end;
  if HandleAllocated then
  begin
    UpdateScrollBar;
    Invalidate;
  end;
end;

procedure TTeThemeDBLookupListBox.MouseDown(Button: TMouseButton; Shift: TShiftState;
  X, Y: Integer);
begin
  if Button = mbLeft then
  begin
    SearchText := '';
    if not FPopup then
    begin
      SetFocus;
      if not HasFocus then Exit;
    end;
    if CanModify then
      if ssDouble in Shift then
      begin
        if FRecordIndex = Y div GetTextHeight then DblClick;
      end else
      begin
        MouseCapture := True;
        FTracking := True;
        SelectItemAt(X, Y);
      end;
  end;
  inherited MouseDown(Button, Shift, X, Y);
end;

procedure TTeThemeDBLookupListBox.MouseMove(Shift: TShiftState; X, Y: Integer);
begin
  if FTracking then
  begin
    SelectItemAt(X, Y);
    FMousePos := Y;
    TimerScroll;
  end;
  inherited MouseMove(Shift, X, Y);
end;

procedure TTeThemeDBLookupListBox.MouseUp(Button: TMouseButton; Shift: TShiftState;
  X, Y: Integer);
begin
  if FTracking then
  begin
    StopTracking;
    SelectItemAt(X, Y);
  end;
  inherited MouseUp(Button, Shift, X, Y);
end;

procedure TTeThemeDBLookupListBox.PaintBuffer;
var
  I, J, W, X, TextWidth, TextHeight, LastFieldIndex: Integer;
  S: string;
  R: TRect;
  Selected: Boolean;
  Field: TField;
  AAlignment: TAlignment;
begin
  inherited ;

  Canvas.Font := Font;
  TextWidth := Canvas.TextWidth('0');
  TextHeight := Canvas.TextHeight('0');
  LastFieldIndex := ListFields.Count - 1;
  if ColorToRGB(Color) <> ColorToRGB(clBtnFace) then
    Canvas.Pen.Color := clBtnFace else
    Canvas.Pen.Color := clBtnShadow;
  for I := 0 to FRowCount - 1 do
  begin
    if Enabled then
      Canvas.Font.Color := Font.Color else
      Canvas.Font.Color := clGrayText;
    Canvas.Brush.Color := Color;
    Selected := not FKeySelected and (I = 0);

    R.Top := GetBorderRect.Top + I * TextHeight;
    R.Bottom := R.Top + TextHeight;
    if I < FRecordCount then
    begin
      ListLink.ActiveRecord := I;
      if not VarIsNull(FKeyValue) and
        VarEquals(FKeyField.Value, FKeyValue) then
      begin
        Canvas.Font.Color := clHighlightText;
        Canvas.Brush.Color := clHighlight;
        Selected := True;
      end;
      R.Right := GetBorderRect.Left;
      for J := 0 to LastFieldIndex do
      begin
        Field := ListFields[J];
        if J < LastFieldIndex then
          W := Field.DisplayWidth * TextWidth + 4 else
          W := ClientWidth - R.Right;
        S := Field.DisplayText;
        X := GetBorderRect.Left + 2;
        AAlignment := Field.Alignment;
        if UseRightToLeftAlignment then ChangeBiDiModeAlignment(AAlignment);
        case AAlignment of
          taRightJustify: X := W - Canvas.TextWidth(S) - 3;
          taCenter: X := (W - Canvas.TextWidth(S)) div 2;
        end;
        R.Left := R.Right;
        R.Right := R.Right + W;
        Canvas.TextRect(R, R.Left + X, R.Top, S);
        if J < LastFieldIndex then
        begin
          Canvas.MoveTo(R.Right, R.Top);
          Canvas.LineTo(R.Right, R.Bottom);
          Inc(R.Right);
          if R.Right >= ClientWidth then Break;
        end;
      end;
    end;
    R.Left := GetBorderRect.Left;
    R.Right := ClientWidth;
    if I >= FRecordCount then Canvas.FillRect(R);
    if Selected and (HasFocus or FPopup) then
      Canvas.DrawFocusRect(R);
  end;
  if FRecordCount <> 0 then ListLink.ActiveRecord := FRecordIndex;
end;

procedure TTeThemeDBLookupListBox.SelectCurrent;
begin
  FLockPosition := True;
  try
    SelectKeyValue(FKeyField.Value);
  finally
    FLockPosition := False;
  end;
end;

procedure TTeThemeDBLookupListBox.SelectItemAt(X, Y: Integer);
var
  Delta: Integer;
begin
  if Y < 0 then Y := 0;
  if Y >= ClientHeight then Y := ClientHeight - 1;
  Delta := Y div GetTextHeight - FRecordIndex;
  ListLink.DataSet.MoveBy(Delta);
  SelectCurrent;
end;

procedure TTeThemeDBLookupListBox.SetBounds(ALeft, ATop, AWidth, AHeight: Integer);
var
  BorderSize, TextHeight, Rows: Integer;
begin
  BorderSize := GetBorderRect.Top + (FHeight - GetBorderRect.Bottom);
  TextHeight := GetTextHeight;
  Rows := (AHeight - BorderSize) div TextHeight;
  if Rows < 1 then Rows := 1;
  FRowCount := Rows;
  if (ListLink <> nil) and (ListLink.BufferCount <> Rows) then
  begin
    ListLink.BufferCount := Rows;
    ListLinkDataChanged;
  end;
  inherited SetBounds(ALeft, ATop, AWidth, Rows * TextHeight + BorderSize);
end;

function TTeThemeDBLookupListBox.UseRightToLeftAlignment: Boolean;
begin
  Result := DBUseRightToLeftAlignment(Self, Field);
end;

procedure TTeThemeDBLookupListBox.SetRowCount(Value: Integer);
begin
  if Value < 1 then Value := 1;
  if Value > 100 then Value := 100;
  Height := Value * GetTextHeight + GetBorderSize;
end;

procedure TTeThemeDBLookupListBox.StopTimer;
begin
  if FTimerActive then
  begin
    KillTimer(Handle, 1);
    FTimerActive := False;
  end;
end;

procedure TTeThemeDBLookupListBox.StopTracking;
begin
  if FTracking then
  begin
    StopTimer;
    FTracking := False;
    MouseCapture := False;
  end;
end;

procedure TTeThemeDBLookupListBox.TimerScroll;
var
  Delta, Distance, Interval: Integer;
begin
  Delta := 0;
  Distance := 0;
  if FMousePos < 0 then
  begin
    Delta := -1;
    Distance := -FMousePos;
  end;
  if FMousePos >= ClientHeight then
  begin
    Delta := 1;
    Distance := FMousePos - ClientHeight + 1;
  end;
  if Delta = 0 then StopTimer else
  begin
    if ListLink.DataSet.MoveBy(Delta) <> 0 then SelectCurrent;
    Interval := 200 - Distance * 15;
    if Interval < 0 then Interval := 0;
    SetTimer(Handle, 1, Interval, nil);
    FTimerActive := True;
  end;
end;

function TTeThemeDBLookupListBox.CreateScrollBar: TControl;
begin
  Result := TTeThemeScrollBar.Create(Self);
  (Result as TTeThemeScrollBar).ThemeEngine := ThemeEngine;
end;

procedure TTeThemeDBLookupListBox.UpdateScrollBar;
var
  Pos, Max: Integer;
  ScrollInfo: TScrollInfo;
begin
  if VScrollBar = nil then Exit;
  
  Pos := 0;
  Max := 0;
  if FRecordCount = FRowCount then
  begin
    Max := 4;
    if not ListLink.DataSet.BOF then
      if not ListLink.DataSet.EOF then Pos := 2 else Pos := 4;
  end;

  if Max = 0 then
    VScrollBar.Enabled := false
  else
  begin
    VScrollBar.Enabled := true;
    ScrollInfo.cbSize := SizeOf(TScrollInfo);
    ScrollInfo.fMask := SIF_POS or SIF_RANGE;
    if not GetScrollInfo(SB_VERT, ScrollInfo) or
      (ScrollInfo.nPos <> Pos) or (ScrollInfo.nMax <> Max) then
    begin
      ScrollInfo.nMin := 0;
      ScrollInfo.nMax := Max;
      ScrollInfo.nPos := Pos;
      SetScrollInfo(SB_VERT, ScrollInfo, True);
    end;
  end;
end;

procedure TTeThemeDBLookupListBox.CMFontChanged(var Message: TMessage);
begin
  inherited;
  Height := Height;
end;

procedure TTeThemeDBLookupListBox.WMCancelMode(var Message: TMessage);
begin
  StopTracking;
  inherited;
end;

procedure TTeThemeDBLookupListBox.WMTimer(var Message: TMessage);
begin
  TimerScroll;
end;

procedure TTeThemeDBLookupListBox.WMVScroll(var Message: TWMVScroll);
begin
  SearchText := '';

  with Message, ListLink.DataSet do
    case ScrollCode of
      SB_LINEUP: MoveBy(-FRecordIndex - 1);
      SB_LINEDOWN: MoveBy(FRecordCount - FRecordIndex);
      SB_PAGEUP: MoveBy(-FRecordIndex - FRecordCount + 1);
      SB_PAGEDOWN: MoveBy(FRecordCount - FRecordIndex + FRecordCount - 2);
      SB_THUMBPOSITION:
        begin
          case Pos of
            0: First;
            1: MoveBy(-FRecordIndex - FRecordCount + 1);
            2: Exit;
            3: MoveBy(FRecordCount - FRecordIndex + FRecordCount - 2);
            4: Last;
          end;
        end;
      SB_BOTTOM: Last;
      SB_TOP: First;
    end;
end;

function TTeThemeDBLookupListBox.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (FDataLink <> nil) and
    FDataLink.ExecuteAction(Action);
end;

function TTeThemeDBLookupListBox.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (FDataLink <> nil) and
    FDataLink.UpdateAction(Action);
end;

{ TTePopupDataList }

constructor TTePopupDataList.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  ControlStyle := ControlStyle + [csNoDesignVisible, csReplicatable];
  FPopup := True;
end;

procedure TTePopupDataList.CreateParams(var Params: TCreateParams);
begin
  inherited CreateParams(Params);
  with Params do
  begin
    Style := WS_POPUP;
    ExStyle := WS_EX_TOOLWINDOW;
    AddBiDiModeExStyle(ExStyle);
    WindowClass.Style := CS_SAVEBITS;
  end;
end;

procedure TTePopupDataList.WMMouseActivate(var Message: TMessage);
begin
  Message.Result := MA_NOACTIVATE;
end;

{ TTeThemeDBLookupComboBox }

constructor TTeThemeDBLookupComboBox.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  BevelKind := kbkSingle;
  BevelWidth := 1;
  BorderWidth := 3;
  
  FDataList := TTePopupDataList.Create(Self);
  FDataList.Visible := False;
  FDataList.Parent := Self;
  FDataList.OnMouseUp := ListMouseUp;

  ControlStyle := ControlStyle + [csReplicatable];
  Width := 145;
  Height := 0;
  FButtonWidth := 15;
  FDropDownRows := 7;
end;

procedure TTeThemeDBLookupComboBox.Loaded;
begin
  inherited Loaded;
  FDataList.Loaded;
end;

procedure TTeThemeDBLookupComboBox.CloseUp(Accept: Boolean);
var
  ListValue: Variant;
begin
  if FListVisible then
  begin
    if GetCapture <> 0 then SendMessage(GetCapture, WM_CANCELMODE, 0, 0);
    ListValue := FDataList.KeyValue;
    SetWindowPos(FDataList.Handle, 0, 0, 0, 0, 0, SWP_NOZORDER or
      SWP_NOMOVE or SWP_NOSIZE or SWP_NOACTIVATE or SWP_HIDEWINDOW);
    FDataList.Visible := false;
    FListVisible := False;
    FDataList.ListSource := nil;
    Invalidate;
    SearchText := '';
    if Accept and CanModify then SelectKeyValue(ListValue);
    if Assigned(FOnCloseUp) then FOnCloseUp(Self);
  end;
end;

procedure TTeThemeDBLookupComboBox.CMBiDiModeChanged(var Message: TMessage);
begin
  inherited;
  FDataList.BiDiMode := BiDiMode;
end;

procedure TTeThemeDBLookupComboBox.CMDialogKey(var Message: TCMDialogKey);
begin
  if (Message.CharCode in [VK_RETURN, VK_ESCAPE]) and FListVisible then
  begin
    CloseUp(Message.CharCode = VK_RETURN);
    Message.Result := 1;
  end else
    inherited;
end;

procedure TTeThemeDBLookupComboBox.CreateParams(var Params: TCreateParams);
begin
  inherited CreateParams(Params);
end;

procedure TTeThemeDBLookupComboBox.DropDown;
var
  P: TPoint;
  I, Y: Integer;
  S: string;
  ADropDownAlign: TDropDownAlign;
begin
  if not FListVisible and ListActive then
  begin
    if Assigned(FOnDropDown) then FOnDropDown(Self);
    FDataList.Color := Color;
    FDataList.Font := Font;
    if FDropDownWidth > 0 then
      FDataList.Width := FDropDownWidth else
      FDataList.Width := Width;
    FDataList.ReadOnly := not CanModify;
    if (ListLink.DataSet.RecordCount > 0) and
       (FDropDownRows > ListLink.DataSet.RecordCount) then
      FDataList.RowCount := ListLink.DataSet.RecordCount else
      FDataList.RowCount := FDropDownRows;
    FDataList.KeyField := FKeyFieldName;
    for I := 0 to ListFields.Count - 1 do
      S := S + TField(ListFields[I]).FieldName + ';';
    FDataList.ListField := S;
    FDataList.ListFieldIndex := ListFields.IndexOf(FListField);
    FDataList.ListSource := ListLink.DataSource;
    FDataList.KeyValue := KeyValue;
    FDataList.ThemeEngine := ThemeEngine;

    P := Parent.ClientToScreen(Point(Left, Top));
    Y := P.Y + Height;
    if Y + FDataList.Height > Screen.Height then Y := P.Y - FDataList.Height;
    ADropDownAlign := FDropDownAlign;
    { This alignment is for the ListField, not the control }
    if DBUseRightToLeftAlignment(Self, FListField) then
    begin
      if ADropDownAlign = daLeft then
        ADropDownAlign := daRight
      else if ADropDownAlign = daRight then
        ADropDownAlign := daLeft;
    end;
    case ADropDownAlign of
      daRight: Dec(P.X, FDataList.Width - Width);
      daCenter: Dec(P.X, (FDataList.Width - Width) div 2);
    end;

    SetWindowPos(FDataList.Handle, HWND_TOP, P.X, Y, 0, 0,
      SWP_NOSIZE or SWP_NOACTIVATE or SWP_SHOWWINDOW);
    FDataList.Visible := true;

    FListVisible := True;
    Repaint;
  end;
end;

procedure TTeThemeDBLookupComboBox.KeyDown(var Key: Word; Shift: TShiftState);
var
  Delta: Integer;
begin
  inherited KeyDown(Key, Shift);
  if ListActive and ((Key = VK_UP) or (Key = VK_DOWN)) then
    if ssAlt in Shift then
    begin
      if FListVisible then CloseUp(True) else DropDown;
      Key := 0;
    end else
      if not FListVisible then
      begin
        if not LocateKey then
          ListLink.DataSet.First
        else
        begin
          if Key = VK_UP then Delta := -1 else Delta := 1;
          ListLink.DataSet.MoveBy(Delta);
        end;
        SelectKeyValue(FKeyField.Value);
        Key := 0;
      end;
  if (Key <> 0) and FListVisible then FDataList.KeyDown(Key, Shift);
end;

procedure TTeThemeDBLookupComboBox.KeyPress(var Key: Char);
begin
  inherited KeyPress(Key);
  if FListVisible then
    if Key in [#13, #27] then
      CloseUp(Key = #13)
    else
      FDataList.KeyPress(Key)
  else
    ProcessSearchKey(Key);
end;

procedure TTeThemeDBLookupComboBox.KeyValueChanged;
begin
  if FLookupMode then
  begin
    FText := FDataField.DisplayText;
    FAlignment := FDataField.Alignment;
  end else
  if ListActive and LocateKey then
  begin
    FText := FListField.DisplayText;
    FAlignment := FListField.Alignment;
  end else
  begin
    FText := '';
    FAlignment := taLeftJustify;
  end;
  Invalidate;
end;

procedure TTeThemeDBLookupComboBox.UpdateListFields;
begin
  inherited;
  KeyValueChanged;
end;

procedure TTeThemeDBLookupComboBox.ListMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  if Button = mbLeft then
    CloseUp(PtInRect(FDataList.ClientRect, Point(X, Y)));
end;

procedure TTeThemeDBLookupComboBox.MouseDown(Button: TMouseButton; Shift: TShiftState;
  X, Y: Integer);
begin
  if Button = mbLeft then
  begin
    SetFocus;
    if not HasFocus then Exit;
    if FListVisible then CloseUp(False) else
      if ListActive then
      begin
        MouseCapture := True;
        FTracking := True;
        TrackButton(X, Y);
        DropDown;
      end;
  end;
  inherited MouseDown(Button, Shift, X, Y);
end;

procedure TTeThemeDBLookupComboBox.MouseMove(Shift: TShiftState; X, Y: Integer);
var
  ListPos: TPoint;
  MousePos: TSmallPoint;
begin
  if FTracking then
  begin
    TrackButton(X, Y);
    if FListVisible then
    begin
      ListPos := FDataList.ScreenToClient(ClientToScreen(Point(X, Y)));
      if PtInRect(FDataList.ClientRect, ListPos) then
      begin
        StopTracking;
        MousePos := PointToSmallPoint(ListPos);
        SendMessage(FDataList.Handle, WM_LBUTTONDOWN, 0, Integer(MousePos));
        Exit;
      end;
    end;
  end;
  inherited MouseMove(Shift, X, Y);
end;

procedure TTeThemeDBLookupComboBox.MouseUp(Button: TMouseButton; Shift: TShiftState;
  X, Y: Integer);
begin
  StopTracking;
  inherited MouseUp(Button, Shift, X, Y);
end;

procedure TTeThemeDBLookupComboBox.PaintBuffer;
var
  W, X, Flags: Integer;
  Text: string;
  AAlignment: TAlignment;
  Selected: Boolean;
  R: TRect;
  State: TTeThemeButtonState;
  Theme: HTheme;
  Part, tState: integer;
begin
  inherited ;

  Canvas.Font.Assign(Font);

  Selected := Focused and (not FListVisible);

  if UseTheme then
  begin
    FButtonWidth := FThemeEngine.Theme.GetMetrix(ngmComboButtonWidth);
    Canvas.Brush.Color := Color;

    if Enabled then
      Canvas.Font.Color := Font.Color
    else
      Canvas.Font.Color := clGrayText;

    if Selected then
    begin
      Canvas.Font.Color := clHighlightText;
      Canvas.Brush.Color := clHighlight;
    end;
  end
  else
  begin
    Canvas.Brush.Color := Color;

    if Enabled then
      Canvas.Font.Color := Font.Color
    else
      Canvas.Font.Color := clGrayText;

    if Selected then
    begin
      Canvas.Font.Color := clHighlightText;
      Canvas.Brush.Color := clHighlight;
    end;
  end;

  if (csPaintCopy in ControlState) and (FDataField <> nil) and
    (FDataField.Lookup) then
  begin
    Text := FDataField.DisplayText;
    AAlignment := FDataField.Alignment;
  end
  else
    begin
      if (csDesigning in ComponentState) and (FDataField = nil) then
        Text := Name
      else
        Text := FText;
      AAlignment := FAlignment;
    end;

  if UseRightToLeftAlignment then ChangeBiDiModeAlignment(AAlignment);

  W := ClientWidth - FButtonWidth;
  X := GetBorderRect.Left;
  case AAlignment of
    taRightJustify: X := W - Canvas.TextWidth(Text);
    taCenter: X := (W - Canvas.TextWidth(Text)) div 2;
  end;

  R := GetBorderRect;
  R.Right := R.Right - FButtonWidth;

  if (SysLocale.MiddleEast) and (BiDiMode = bdRightToLeft) then
  begin
    Inc(X, FButtonWidth);
    Inc(R.Left, FButtonWidth);
    R.Right := GetBorderRect.Right;
  end;

  Canvas.TextRect(R, X, GetBorderRect.Top, Text);
  if Selected then Canvas.DrawFocusRect(R);

  SetRect(R, W, GetBorderRect.Top, ClientWidth, ClientHeight);

  if (SysLocale.MiddleEast) and (BiDiMode = bdRightToLeft) then
  begin
    R.Left := GetBorderRect.Left;
    R.Right:= R.Left + FButtonWidth;
  end;

  { Draw Button }
  if UseTheme then
  begin
    if not ListActive then
      State := ngsDisabled
    else
      if FPressed then
        State := ngsPressed
      else
        State := ngsNormal;

    InflateRect(R, 0, BorderWidth);
    if (SysLocale.MiddleEast) and (BiDiMode = bdRightToLeft) then
      OffsetRect(R, -BorderWidth, 0)
    else
      OffsetRect(R, BorderWidth, 0); 

    FThemeEngine.Theme.DrawComboButton(Canvas, R, State);
  end
  else
    if UseThemes then
    begin
      { XP Style }
      InflateRect(R, 0, BorderWidth);
      if (SysLocale.MiddleEast) and (BiDiMode = bdRightToLeft) then
        OffsetRect(R, -BorderWidth, 0)
      else
        OffsetRect(R, BorderWidth, 0);

      Theme := OpenThemeData(0, 'ComboBox');
      Part := integer(CP_DROPDOWNBUTTON);

      if not Enabled then
        tState := integer(CBXS_DISABLED)
      else
        if FPressed then
          tState := integer(CBXS_PRESSED)
        else
          tState := integer(CBXS_NORMAL);

      DrawThemeBackground(Theme, Canvas.Handle, Part, tState, R, nil);

      CloseThemeData(Theme);
    end
    else
    begin
      { Default }
      if not ListActive then
        Flags := DFCS_SCROLLCOMBOBOX or DFCS_INACTIVE
      else if FPressed then
        Flags := DFCS_SCROLLCOMBOBOX or DFCS_FLAT or DFCS_PUSHED
      else
        Flags := DFCS_SCROLLCOMBOBOX;

      InflateRect(R, 0, BorderWidth);
      if (SysLocale.MiddleEast) and (BiDiMode = bdRightToLeft) then
        OffsetRect(R, -BorderWidth, 0)
      else
        OffsetRect(R, BorderWidth, 0);
      DrawFrameControl(Canvas.Handle, R, DFC_SCROLL, Flags);
    end;
end;

procedure TTeThemeDBLookupComboBox.SetBounds(ALeft, ATop, AWidth, AHeight: Integer);
begin
  inherited SetBounds(ALeft, ATop, AWidth, GetTextHeight + (GetBorderRect.Top + (FHeight - GetBorderRect.Bottom)));
end;

function TTeThemeDBLookupComboBox.UseRightToLeftAlignment: Boolean;
begin
  Result := DBUseRightToLeftAlignment(Self, Field);
end;

procedure TTeThemeDBLookupComboBox.StopTracking;
begin
  if FTracking then
  begin
    TrackButton(-1, -1);
    FTracking := False;
    MouseCapture := False;
  end;
end;

procedure TTeThemeDBLookupComboBox.TrackButton(X, Y: Integer);
var
  NewState: Boolean;
begin
  NewState := PtInRect(Rect(ClientWidth - FButtonWidth, 0, ClientWidth,
    ClientHeight), Point(X, Y));
  if FPressed <> NewState then
  begin
    FPressed := NewState;
    Repaint;
  end;
end;

procedure TTeThemeDBLookupComboBox.CMCancelMode(var Message: TCMCancelMode);
begin
{  if (Message.Sender <> Self) and (Message.Sender <> FDataList) then
    CloseUp(False);}
end;

procedure TTeThemeDBLookupComboBox.CMFontChanged(var Message: TMessage);
begin
  inherited;
  Height := 0;
end;

procedure TTeThemeDBLookupComboBox.CMGetDataLink(var Message: TMessage);
begin
  Message.Result := Integer(FDataLink);
end;

procedure TTeThemeDBLookupComboBox.WMCancelMode(var Message: TMessage);
begin
  StopTracking;
  inherited;
end;

procedure TTeThemeDBLookupComboBox.WMKillFocus(var Message: TWMKillFocus);
begin
  inherited;
  CloseUp(False);
end;

function TTeThemeDBLookupComboBox.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (FDataLink <> nil) and
    FDataLink.ExecuteAction(Action);
end;

function TTeThemeDBLookupComboBox.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (FDataLink <> nil) and
    FDataLink.UpdateAction(Action);
end;

{ TTeThemeDBEditButton }
 
constructor TTeThemeDBEditButton.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  inherited ReadOnly := True;
  FDataLink := TFieldDataLink.Create;
  FDataLink.Control := Self;
  FDataLink.OnDataChange := DataChange;
  FDataLink.OnEditingChange := EditingChange;
  FDataLink.OnUpdateData := UpdateData;
  FDataLink.OnActiveChange := ActiveChange;
end;
 
destructor TTeThemeDBEditButton.Destroy;
begin
  FDataLink.Free;
  FDataLink := nil;
  inherited Destroy;
end;
 
procedure TTeThemeDBEditButton.Loaded;
begin
  inherited Loaded;
  ResetMaxLength;
  if (csDesigning in ComponentState) then DataChange(Self);
end;
 
procedure TTeThemeDBEditButton.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited Notification(AComponent, Operation);
  if (Operation = opRemove) and (FDataLink <> nil) and
    (AComponent = DataSource) then DataSource := nil;
end;
 
function TTeThemeDBEditButton.UseRightToLeftAlignment: Boolean;
begin
  Result := DBUseRightToLeftAlignment(Self, Field);
end;
 
procedure TTeThemeDBEditButton.KeyDown(var Key: Word; Shift: TShiftState);
begin
  inherited KeyDown(Key, Shift);
  if (Key = VK_DELETE) or ((Key = VK_INSERT) and (ssShift in Shift)) then
    FDataLink.Edit;
end;
 
procedure TTeThemeDBEditButton.KeyPress(var Key: Char);
begin
  if (Key in [#32..#255]) and (FDataLink.Field <> nil) and
    not FDataLink.Field.IsValidChar(Key) then
  begin
    MessageBeep(0);
    Key := #0;
  end;
  case Key of
    ^H, ^V, ^X, #32..#255:
      FDataLink.Edit;
    #27:
      begin
        FDataLink.Reset;
        SelectAll;
        Key := #0;
      end;
  end;
 
  inherited KeyPress(Key);
end;
 
procedure TTeThemeDBEditButton.Change;
begin
  if FDataChanging then
  begin
    inherited Change;
    CaretPosition := 0;
  end
  else
  begin
    FDataLink.Modified;
    inherited Change;
  end;
end;
 
procedure TTeThemeDBEditButton.ActiveChange(Sender: TObject);
begin
  ResetMaxLength;
end;
 
procedure TTeThemeDBEditButton.DataChange(Sender: TObject);
begin
  FDataChanging := true;
  try
    if FDataLink.Field <> nil then
    begin
      if TextAlignment <> FDataLink.Field.Alignment then
      begin
        Text := '';  {forces update}
        TextAlignment := FDataLink.Field.Alignment;
      end;
  //    EditMask := FDataLink.Field.EditMask;
      if not (csDesigning in ComponentState) then
      begin
        if (FDataLink.Field.DataType in [ftString, ftWideString]) and (MaxLength = 0) then
          MaxLength := FDataLink.Field.Size;
      end;
      if Focused and FDataLink.CanModify then
      begin
        Text := FDataLink.Field.Text
      end
      else
      begin
        Text := FDataLink.Field.DisplayText;
  {      if FDataLink.Editing and FDataLink.FModified then
          Modified := True;}
      end;
    end
    else
    begin
      TextAlignment := taLeftJustify;
  //    EditMask := '';
      if csDesigning in ComponentState then
        Text := Name
      else
        Text := '';
    end;
  finally
    FDataChanging := false;
  end;
end;
 
procedure TTeThemeDBEditButton.EditingChange(Sender: TObject);
begin
  inherited ReadOnly := not FDataLink.Editing;
end;
 
procedure TTeThemeDBEditButton.UpdateData(Sender: TObject);
begin
//  ValidateEdit;
  FDataLink.Field.Text := Text;
end;
 
procedure TTeThemeDBEditButton.WMUndo(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;
 
procedure TTeThemeDBEditButton.WMPaste(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;
 
procedure TTeThemeDBEditButton.WMCut(var Message: TMessage);
begin
  FDataLink.Edit;
  inherited;
end;
 
procedure TTeThemeDBEditButton.HasFocus;
begin
  inherited;
  if SysLocale.FarEast and FDataLink.CanModify then
    inherited ReadOnly := False;
end;
 
procedure TTeThemeDBEditButton.KillFocus;
begin
  try
    FDataLink.UpdateRecord;
  except
    SelectAll;
    SetFocus;
    raise;
  end;
  inherited ;
end;
 
procedure TTeThemeDBEditButton.CMGetDataLink(var Message: TMessage);
begin
  Message.Result := Integer(FDataLink);
end;
 
function TTeThemeDBEditButton.ExecuteAction(Action: TBasicAction): Boolean;
begin
  Result := inherited ExecuteAction(Action) or (FDataLink <> nil) and
    FDataLink.ExecuteAction(Action);
end;
 
function TTeThemeDBEditButton.UpdateAction(Action: TBasicAction): Boolean;
begin
  Result := inherited UpdateAction(Action) or (FDataLink <> nil) and
    FDataLink.UpdateAction(Action);
end;
 
procedure TTeThemeDBEditButton.ResetMaxLength;
var
  F: TField;
begin
  if (MaxLength > 0) and Assigned(DataSource) and Assigned(DataSource.DataSet) then
  begin
    F := DataSource.DataSet.FindField(DataField);
    if Assigned(F) and (F.DataType in [ftString, ftWideString]) and (F.Size = MaxLength) then
      MaxLength := 0;
  end;
end;
 
function TTeThemeDBEditButton.GetDataSource: TDataSource;
begin
  Result := FDataLink.DataSource;
end;
 
procedure TTeThemeDBEditButton.SetDataSource(Value: TDataSource);
begin
  if not (FDataLink.DataSourceFixed and (csLoading in ComponentState)) then
    FDataLink.DataSource := Value;
  if Value <> nil then Value.FreeNotification(Self);
end;
 
function TTeThemeDBEditButton.GetDataField: string;
begin
  Result := FDataLink.FieldName;
end;
 
procedure TTeThemeDBEditButton.SetDataField(const Value: string);
begin
  if not (csDesigning in ComponentState) then
    ResetMaxLength;
  FDataLink.FieldName := Value;
end;
 
function TTeThemeDBEditButton.GetReadOnly: Boolean;
begin
  Result := FDataLink.ReadOnly;
end;
 
procedure TTeThemeDBEditButton.SetReadOnly(Value: Boolean);
begin
  FDataLink.ReadOnly := Value;
end;
 
function TTeThemeDBEditButton.GetField: TField;
begin
  Result := FDataLink.Field;
end;

end.
