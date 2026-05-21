$mdbPath = 'C:\temp\Bearing.mdb'
$conn = New-Object System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$mdbPath")
$conn.Open()
$schema = $conn.GetOleDbSchemaTable([System.Data.OleDb.OleDbSchemaGuid]::Tables, @())
Write-Output "=== TABLES ==="
foreach ($row in $schema.Rows) {
    if ($row['TABLE_TYPE'] -eq 'TABLE') {
        Write-Output $row['TABLE_NAME']
    }
}

# Helper function to read table
function Read-Table($name, $top) {
    Write-Output "`n=== $name (top $top) ==="
    $sql = "SELECT * FROM [$name]"
    if ($top -gt 0) { $sql = "SELECT TOP $top * FROM [$name]" }
    $cmd = New-Object System.Data.OleDb.OleDbCommand($sql, $conn)
    $reader = $cmd.ExecuteReader()
    # Column names
    $cols = @()
    for ($i=0; $i -lt $reader.FieldCount; $i++) { $cols += $reader.GetName($i) }
    Write-Output ($cols -join '|')
    $count = 0
    while ($reader.Read()) {
        $vals = @()
        for ($i=0; $i -lt $reader.FieldCount; $i++) {
            $v = $reader.GetValue($i)
            if ($v -eq [DBNull]::Value) { $vals += '' } else { $vals += $v }
        }
        Write-Output ($vals -join '|')
        $count++
    }
    Write-Output "Total rows shown: $count"
    $reader.Close()
}

Read-Table '深沟球轴承' 50
Read-Table '角接触球轴承' 50
Read-Table '圆锥滚子轴承' 50
Read-Table '推力球轴承' 30
Read-Table '推力滚子轴承' 30
Read-Table '调心球轴承' 0
Read-Table 'SGQXY' 0
Read-Table 'JQa15XY' 0
Read-Table 'JQa10XY' 0
Read-Table 'JQa5XY' 0
Read-Table 'JQ20a45XY' 0
Read-Table 'TCJXY' 0
Read-Table 'TLGXY' 0
Read-Table 'TLQXY' 0

$conn.Close()
