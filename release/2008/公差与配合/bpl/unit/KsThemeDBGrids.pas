{==============================================================================

  ThemeEngine's DB Grids
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: KsThemeDBGrids.pas,v 1.3 2002/08/13 07:59:09 Evgeny Exp $

===============================================================================}

unit ksthemedbgrids;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses Windows, SysUtils, Messages, Classes, Controls, Forms, Graphics, Menus,
   StdCtrls, ExtCtrls, Buttons, ComCtrls, DB, DBCtrls, DBGrids, Grids, ImgList,
   te_controls, KsThemeEngine, KsThemeVersion, KsThemeScrollBars, KsThemeThemes;

type

{ TTeThemeDBGrid class }

  TTeThemeDBGrid = class(TDBGrid)
  private
    FIndicators: TImageList;
    FTitleOffset: Byte;
    FSelRow: Integer;
    FSubScrollBars: TTeSubScrollBar;
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure MyDrawCell(ACol, ARow: Integer; ARect: TRect;
      AState: TGridDrawState);
  protected
    function UseTheme: boolean;
    { KSDev's ScrollBar subclassing }
    procedure WndProc(var Message: TMessage); override;
    procedure SetParent(AParent: TWinControl); override;
    procedure CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent); virtual;
    { KSDev's Paint }
    procedure PaintBorder(Canvas: TCanvas; ARect: TRect); virtual;
    procedure DrawCell(ACol, ARow: Longint; ARect: TRect; AState: TGridDrawState); override;
    { Grids }
    property SubScrollBars: TTeSubScrollBar read FSubScrollBars;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
  published
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
  end;

implementation {===============================================================}

uses te_winxp, Math;

{$R *.res}

const
  bmArrow = 'TE_DBGARROW';
  bmEdit = 'TE_DBEDIT';
  bmInsert = 'TE_DBINSERT';
  bmMultiDot = 'TE_DBMULTIDOT';
  bmMultiArrow = 'TE_DBMULTIARROW';

var
  DrawBitmap: TBitmap;
  UserCount: Integer;

procedure UsesBitmap;
begin
  if UserCount = 0 then
    DrawBitmap := TBitmap.Create;
  Inc(UserCount);
end;

procedure ReleaseBitmap;
begin
  Dec(UserCount);
  if UserCount = 0 then DrawBitmap.Free;
end;

procedure WriteText(ACanvas: TCanvas; ARect: TRect; DX, DY: Integer;
  const Text: string; Alignment: TAlignment; ARightToLeft: Boolean);
const
  AlignFlags : array [TAlignment] of Integer =
    ( DT_LEFT or DT_WORDBREAK or DT_EXPANDTABS or DT_NOPREFIX,
      DT_RIGHT or DT_WORDBREAK or DT_EXPANDTABS or DT_NOPREFIX,
      DT_CENTER or DT_WORDBREAK or DT_EXPANDTABS or DT_NOPREFIX );
  RTL: array [Boolean] of Integer = (0, DT_RTLREADING);
var
  B, R: TRect;
  Hold, Left: Integer;
  I: TColorRef;
begin
  I := ColorToRGB(ACanvas.Brush.Color);
  if GetNearestColor(ACanvas.Handle, I) = I then
  begin                       { Use ExtTextOut for solid colors }
    { In BiDi, because we changed the window origin, the text that does not
      change alignment, actually gets its alignment changed. }
    if (ACanvas.CanvasOrientation = coRightToLeft) and (not ARightToLeft) then
      ChangeBiDiModeAlignment(Alignment);
    case Alignment of
      taLeftJustify:
        Left := ARect.Left + DX;
      taRightJustify:
        Left := ARect.Right - ACanvas.TextWidth(Text) - 3;
    else { taCenter }
      Left := ARect.Left + (ARect.Right - ARect.Left) shr 1
        - (ACanvas.TextWidth(Text) shr 1);
    end;
    ACanvas.TextRect(ARect, Left, ARect.Top + DY, Text);
  end
  else begin                  { Use FillRect and Drawtext for dithered colors }
    DrawBitmap.Canvas.Lock;
    try
      with DrawBitmap, ARect do { Use offscreen bitmap to eliminate flicker and }
      begin                     { brush origin tics in painting / scrolling.    }
        Width := Max(Width, Right - Left);
        Height := Max(Height, Bottom - Top);
        R := Rect(DX, DY, Right - Left - 1, Bottom - Top - 1);
        B := Rect(0, 0, Right - Left, Bottom - Top);
      end;
      with DrawBitmap.Canvas do
      begin
        Font := ACanvas.Font;
        Font.Color := ACanvas.Font.Color;
        Brush := ACanvas.Brush;
        Brush.Style := bsSolid;
        FillRect(B);
        SetBkMode(Handle, TRANSPARENT);
        if (ACanvas.CanvasOrientation = coRightToLeft) then
          ChangeBiDiModeAlignment(Alignment);
        Windows.DrawText(Handle, PChar(Text), Length(Text), R,
          AlignFlags[Alignment] or RTL[ARightToLeft]);
      end;
      if (ACanvas.CanvasOrientation = coRightToLeft) then  
      begin
        Hold := ARect.Left;
        ARect.Left := ARect.Right;
        ARect.Right := Hold;
      end;
      ACanvas.CopyRect(ARect, DrawBitmap.Canvas, B);
    finally
      DrawBitmap.Canvas.Unlock;
    end;
  end;
end;

{ TTeThemeDBGrid ===============================================================}

constructor TTeThemeDBGrid.Create(AOwner: TComponent);
var
  Bmp: TBitmap;
begin
  inherited Create(AOwner);
  { Sub class scrollbars }
  FSubScrollBars := TTeSubScrollBar.Create(Self, CreateScrollBar);

  Bmp := TBitmap.Create;
  try
    Bmp.LoadFromResourceName(HInstance, bmArrow);
    FIndicators := TImageList.CreateSize(Bmp.Width, Bmp.Height);
    FIndicators.AddMasked(Bmp, clWhite);
    Bmp.LoadFromResourceName(HInstance, bmEdit);
    FIndicators.AddMasked(Bmp, clWhite);
    Bmp.LoadFromResourceName(HInstance, bmInsert);
    FIndicators.AddMasked(Bmp, clWhite);
    Bmp.LoadFromResourceName(HInstance, bmMultiDot);
    FIndicators.AddMasked(Bmp, clWhite);
    Bmp.LoadFromResourceName(HInstance, bmMultiArrow);
    FIndicators.AddMasked(Bmp, clWhite);
  finally
    Bmp.Free;
  end;
  FTitleOffset := 1;
end;

destructor TTeThemeDBGrid.Destroy;
begin
  FSubScrollBars.Control := nil;
  FSubScrollBars.Free;
  FIndicators.Free;
  inherited Destroy;
end;

function TTeThemeDBGrid.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeDBGrid.SetParent(AParent: TWinControl);
begin
  inherited SetParent(AParent);
  if FSubScrollBars <> nil then FSubScrollBars.SetParent(AParent);
end;

procedure TTeThemeDBGrid.CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent);
begin
  AScrollBar := TTeThemeScrollBar.Create(AOwner);
end;

procedure TTeThemeDBGrid.WndProc(var Message: TMessage);
var
  Canvas: TCanvas;
  R: TRect;
begin
  if FSubScrollBars <> nil then FSubScrollBars.SubWndProc(Message);

  case Message.Msg of
    WM_NCPAINT:
      begin
        GetWindowRect(Handle, R);
        OffsetRect(R, -R.Left, -R.Top);

        if BorderStyle = bsNone then
          InflateRect(R, 2, 2);

        Canvas := TCanvas.Create;
        Canvas.Handle := GetWindowDC(Handle);

        ExcludeClipRect(Canvas.Handle, R.Left + 2, R.Top + 2, R.Left + 2 + ClientWidth, R.Top + 2 + ClientHeight);

        PaintBorder(Canvas, R);

        Canvas.Handle := 0;
        Canvas.Free;

        Message.Result := 0;
      end;
  else
    inherited ;
  end;
end;

procedure TTeThemeDBGrid.PaintBorder(Canvas: TCanvas; ARect: TRect);
var
  Theme: HTheme;
  Part, ThemeState: integer;
  R: TRect;
  RColor, SColor: TColor;
  i: integer;
begin
  DefaultDrawing := not ((FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme) and not (csDestroying in ComponentState));
      
  if UseTheme then
  begin
    FThemeEngine.Theme.DrawScrollBox(Canvas, ARect, Enabled);
  end
  else
    if UseThemes then
    begin
      Theme := OpenThemeData(0, 'Edit');
      Part := integer(EP_EDITText);

      if not Enabled then
        ThemeState := integer(ETS_DISABLED)
      else
        if Focused then
          ThemeState := integer(ETS_SELECTED)
        else
          ThemeState := integer(ETS_NORMAL);

      DrawThemeBackground(Theme, Canvas.Handle, Part, ThemeState, ARect, nil);

      CloseThemeData(Theme);
    end
    else
    begin
      DrawEdge(Canvas, ARect, clBtnShadow, clBtnHighlight);
      InflateRect(ARect, -1, -1);
      DrawEdge(Canvas, ARect, cl3DDkShadow, clBtnFace);

      FillRect(Canvas, Rect(ARect.Right - 20, ARect.Bottom - 20, ARect.Right, ARect.Bottom), clBtnFace);
    end;
end;

function TTeThemeDBGrid.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeDBGrid.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  DefaultDrawing := not ((FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme) and not (csDestroying in ComponentState));

  if SubScrollBars <> nil then
  begin
    TTeThemeScrollBar(SubScrollBars.HScrollBar.ScrollBar).ThemeEngine := Value;
    TTeThemeScrollBar(SubScrollBars.VScrollBar.ScrollBar).ThemeEngine := Value;
  end;

  Invalidate;
end;

procedure TTeThemeDBGrid.SetVersion(const Value: TTeThemeVersion);
begin
end;

procedure TTeThemeDBGrid.MyDrawCell(ACol, ARow: Longint; ARect: TRect; AState: TGridDrawState);
var
  FrameOffs: Byte;

  function RowIsMultiSelected: Boolean;
  var
    Index: Integer;
  begin
    Result := (dgMultiSelect in Options) and Datalink.Active and
      SelectedRows.Find(Datalink.Datasource.Dataset.Bookmark, Index);
  end;

  procedure DrawTitleCell(ACol, ARow: Integer; Column: TColumn; var AState: TGridDrawState);
  const
    ScrollArrows: array [Boolean, Boolean] of Integer =
      ((DFCS_SCROLLRIGHT, DFCS_SCROLLLEFT), (DFCS_SCROLLLEFT, DFCS_SCROLLRIGHT));
  var
    MasterCol: TColumn;
    TitleRect, TextRect, ButtonRect: TRect;
    I: Integer;
    InBiDiMode: Boolean;
  begin
    TitleRect := CalcTitleRect(Column, ARow, MasterCol);

    if MasterCol = nil then
    begin
      Exit;
    end;

    Canvas.Brush.Style := bsClear;

    if [dgRowLines, dgColLines] * Options = [dgRowLines, dgColLines] then
      InflateRect(TitleRect, -1, -1);
    TextRect := TitleRect;
    I := GetSystemMetrics(SM_CXHSCROLL);
    if ((TextRect.Right - TextRect.Left) > I) and MasterCol.Expandable then
    begin
      Dec(TextRect.Right, I);
      ButtonRect := TitleRect;
      ButtonRect.Left := TextRect.Right;
      I := SaveDC(Canvas.Handle);
      try
        Canvas.FillRect(ButtonRect);
        InflateRect(ButtonRect, -1, -1);
        IntersectClipRect(Canvas.Handle, ButtonRect.Left,
          ButtonRect.Top, ButtonRect.Right, ButtonRect.Bottom);
        InflateRect(ButtonRect, 1, 1);
        { DrawFrameControl doesn't draw properly when orienatation has changed.
          It draws as ExtTextOut does. }
        InBiDiMode := Canvas.CanvasOrientation = coRightToLeft;
        if InBiDiMode then { stretch the arrows box }
          Inc(ButtonRect.Right, GetSystemMetrics(SM_CXHSCROLL) + 4);
        DrawFrameControl(Canvas.Handle, ButtonRect, DFC_SCROLL,
          ScrollArrows[InBiDiMode, MasterCol.Expanded] or DFCS_FLAT);
      finally
        RestoreDC(Canvas.Handle, I);
      end;
    end;
    with MasterCol.Title do
      WriteText(Canvas, TextRect, FrameOffs, FrameOffs, Caption, Alignment,
        IsRightToLeft);
    AState := AState - [gdFixed];  // prevent box drawing later
  end;

var
  OldActive: Integer;
  Indicator: Integer;
  Highlight: Boolean;
  Value: string;
  DrawColumn: TColumn;
  MultiSelected: Boolean;
  ALeft: Integer;
begin
  if csLoading in ComponentState then
  begin
    Canvas.Brush.Color := Color;
    Canvas.FillRect(ARect);
    Exit;
  end;

  Dec(ARow, FTitleOffset);
  Dec(ACol, IndicatorOffset);

  if (gdFixed in AState) and ([dgRowLines, dgColLines] * Options =
    [dgRowLines, dgColLines]) then
  begin
    InflateRect(ARect, -1, -1);
    FrameOffs := 1;
  end
  else
    FrameOffs := 2;

  if (gdFixed in AState) and (ACol < 0) then
  begin
    if Assigned(DataLink) and DataLink.Active  then
    begin
      MultiSelected := False;
      if ARow >= 0 then
      begin
        OldActive := DataLink.ActiveRecord;
        try
          Datalink.ActiveRecord := ARow;
          MultiSelected := RowIsMultiselected;
        finally
          Datalink.ActiveRecord := OldActive;
        end;
      end;
      if (ARow = DataLink.ActiveRecord) or MultiSelected then
      begin
        Indicator := 0;
        if DataLink.DataSet <> nil then
          case DataLink.DataSet.State of
            dsEdit: Indicator := 1;
            dsInsert: Indicator := 2;
            dsBrowse:
              if MultiSelected then
                if (ARow <> Datalink.ActiveRecord) then
                  Indicator := 3
                else
                  Indicator := 4;  // multiselected and current row
          end;
        FIndicators.BkColor := FixedColor;
        FIndicators.DrawingStyle := dsTransparent;
        ALeft := ARect.Right - FIndicators.Width - FrameOffs;
        if Canvas.CanvasOrientation = coRightToLeft then Inc(ALeft);
        FIndicators.Draw(Canvas, ALeft,
          (ARect.Top + ARect.Bottom - FIndicators.Height) shr 1, Indicator, True);
        if ARow = Datalink.ActiveRecord then
          FSelRow := ARow + FTitleOffset;
      end;
    end;
  end
  else
    with Canvas do
    begin
      DrawColumn := Columns[ACol];
      if not DrawColumn.Showing then Exit;
      if ARow < 0 then
        DrawTitleCell(ACol, ARow + FTitleOffset, DrawColumn, AState)
      else
        if (DataLink = nil) or not DataLink.Active then
          {FillRect(ARect)}
        else
        begin
          Value := '';
          OldActive := DataLink.ActiveRecord;
          try
            DataLink.ActiveRecord := ARow;
            if Assigned(DrawColumn.Field) then
              Value := DrawColumn.Field.DisplayText;
            Highlight := HighlightCell(ACol, ARow, Value, AState);

            { Draw text }
            Brush.Style := bsClear;
            WriteText(Canvas, ARect, 2, 2, Value, DrawColumn.Alignment,
              UseRightToLeftAlignmentForField(DrawColumn.Field, DrawColumn.Alignment));
          
            if Columns.State = csDefault then
              DrawDataCell(ARect, DrawColumn.Field, AState);
            DrawColumnCell(ARect, ACol, DrawColumn, AState);
          finally
            DataLink.ActiveRecord := OldActive;
          end;
        end;
    end;
end;

procedure TTeThemeDBGrid.DrawCell(ACol, ARow: Integer; ARect: TRect;
  AState: TGridDrawState);
begin
  if UseTheme then
  begin
    FThemeEngine.Theme.DrawGridCell(Canvas, ARect, AState);

    if gdFocused in AState then
      Canvas.Font.Color := clHighlightText
    else
      Canvas.Font.Color := Font.Color;

    MyDrawCell(ACol, ARow, ARect, AState);
  end
  else
    inherited;
end;

end.
