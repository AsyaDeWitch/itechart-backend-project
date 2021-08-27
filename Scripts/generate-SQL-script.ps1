function Generate-SQL {
    # [CmdletBinding(SupportsShouldProcess=$true)]
    param(
        [Parameter(
            Mandatory=$true,
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true
        )]
        [String[]]
        $Path ,
    
        [String]
        $Name
    )
    
    Process {
        foreach($p in $Path) {
            if(Test-Path $p) {
                dotnet ef migrations add $Name --project $p
                Write-Host "Add migration script executed successfully" -BackgroundColor Green -ForegroundColor Black
            }
            else{
                Write-Host "Add migration script failed (wrong path)" -BackgroundColor Red -ForegroundColor Black
            }
        }
    }
}