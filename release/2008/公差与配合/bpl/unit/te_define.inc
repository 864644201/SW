{==============================================================================

  Define v. 3.0.0

  Copyright (C) 2000-2001 by Evgeny Kryukov
  All rights reserved

  $Id: te_define.inc,v 1.10.2.1 2003/01/23 16:14:46 evgeny Exp $

===============================================================================}

{rem $DEFINE KS_CLX} // CLX Only

{$IFDEF WIN32}
  {$DEFINE KS_WIN}
{$ELSE}
  {$DEFINE KS_LINUX}
{$ENDIF}

{$IFDEF MSWINDOWS}
  {$DEFINE KS_WIN}
{$ENDIF}

{$IFDEF LINUX}
  {$DEFINE KS_LINUX}
{$ENDIF}

{$IFDEF KS_LINUX} { ================= LINUX Target ============================}

  {$IFDEF VER140}
    {$DEFINE KS_COMPILER6}

    {$DEFINE KS_KYLIX}
    {$DEFINE KS_KYLIX1}
  {$ENDIF}

{$ELSE} { =========================== Windows Target ==========================}

  {$IFDEF VER150}
    {$DEFINE KS_COMPILER7}
    {$IFDEF BCB}
      {$DEFINE KS_CBUILDER7}
      {$DEFINE KS_CBUILDER}
    {$ELSE}
      {$DEFINE KS_DELPHI7}
      {$DEFINE KS_DELPHI}
    {$ENDIF}
  {$ENDIF}

  {$IFDEF VER140}
    {$DEFINE KS_COMPILER6}
    {$IFDEF BCB}
      {$DEFINE KS_CBUILDER6}
      {$DEFINE KS_CBUILDER}
    {$ELSE}
      {$DEFINE KS_DELPHI6}
      {$DEFINE KS_DELPHI}
    {$ENDIF}
  {$ENDIF}

  {$IFDEF VER130}
    {$DEFINE KS_COMPILER5}
    {$IFDEF BCB}
      {$DEFINE KS_CBUILDER5}
      {$DEFINE KS_CBUILDER}
    {$ELSE}
      {$DEFINE KS_DELPHI5}
      {$DEFINE KS_DELPHI}
    {$ENDIF}
  {$ENDIF}

  {$IFDEF VER125}
    {$DEFINE KS_COMPILER4}
    {$DEFINE KS_CBUILDER4}
    {$DEFINE KS_CBUILDER}
  {$ENDIF}

  {$IFDEF VER120}
    {$DEFINE KS_COMPILER4}
    {$DEFINE KS_DELPHI4}
    {$DEFINE KS_DELPHI}
  {$ENDIF}

  {$IFDEF KS_DELPHI7}
    {$DEFINE KS_DELPHI7_UP}
    {$DEFINE KS_DELPHI6_UP}
    {$DEFINE KS_DELPHI5_UP}
    {$DEFINE KS_DELPHI4_UP}
  {$ENDIF}

  {$IFDEF KS_DELPHI6}
    {$DEFINE KS_DELPHI6_UP}
    {$DEFINE KS_DELPHI5_UP}
    {$DEFINE KS_DELPHI4_UP}
  {$ENDIF}

  {$IFDEF KS_DELPHI5}
    {$DEFINE KS_DELPHI5_UP}
    {$DEFINE KS_DELPHI4_UP}
  {$ENDIF}

  {$IFDEF KS_DELPHI4}
    {$DEFINE KS_DELPHI4_UP}
  {$ENDIF}

  {$IFDEF KS_CBUILDER6}
    {$DEFINE KS_CBUILDER6_UP}
    {$DEFINE KS_CBUILDER5_UP}
    {$DEFINE KS_CBUILDER4_UP}
  {$ENDIF}

  {$IFDEF KS_CBUILDER5}
    {$DEFINE KS_CBUILDER5_UP}
    {$DEFINE KS_CBUILDER4_UP}
  {$ENDIF}

  {$IFDEF KS_CBUILDER4}
    {$DEFINE KS_CBUILDER4_UP}
  {$ENDIF}
{$ENDIF}

{ ------------ !! COMPILER COMPATIBLE !! ------------ }

{$IFDEF KS_COMPILER7}
  {$DEFINE KS_COMPILER7_UP}
  {$DEFINE KS_COMPILER6_UP}
  {$DEFINE KS_COMPILER5_UP}
  {$DEFINE KS_COMPILER4_UP}
{$ENDIF}

{$IFDEF KS_COMPILER6}
  {$DEFINE KS_COMPILER6_UP}
  {$DEFINE KS_COMPILER5_UP}
  {$DEFINE KS_COMPILER4_UP}
{$ENDIF}

{$IFDEF KS_COMPILER5}
  {$DEFINE KS_COMPILER5_UP}
  {$DEFINE KS_COMPILER4_UP}
{$ENDIF}

{$IFDEF KS_COMPILER4}
  {$DEFINE KS_COMPILER4_UP}
{$ENDIF}

{ ------------ !! DO NOT CHANGE DEFINES BELOW THIS LINE !! ------------ }

{$IFDEF KS_CBUILDER}
  {$ObjExportAll On}
{$ENDIF}

{$ALIGN ON}
{$BOOLEVAL OFF}
{$LONGSTRINGS ON}
{$WRITEABLECONST ON}

{ ------------ !! DO NOT CHANGE DEFINES BELOW THIS LINE !! ------------ }

{$IFNDEF KS_DEBUG}
  {$WARNINGS OFF}
  {$HINTS OFF}
{$ENDIF}

{$B-} {- Complete Boolean Evaluation }
{$R-} {- Range-Checking }
{$V-} {- Var-String Checking }
{$T-} {- Typed @ operator }
{$X+} {- Extended syntax }
{$P+} {- Open string params }
{$J+} {- Enable writeable const}

{$IFDEF KS_COMPILER7}
  {$WARN SYMBOL_DEPRECATED OFF}
  {$WARN UNSAFE_TYPE OFF}
  {$WARN UNSAFE_CODE OFF}
  {$WARN UNSAFE_CAST OFF}
{$ENDIF}
