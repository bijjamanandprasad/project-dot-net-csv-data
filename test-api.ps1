# Test script for Client API (PowerShell version)
# Make sure the API is running before executing this script

$BaseUrl = "http://localhost:5000"

Write-Host "Testing Client API endpoints..." -ForegroundColor Green
Write-Host "================================" -ForegroundColor Green

# Test 1: Get US clients
Write-Host "1. Testing GET /clients?country_code=US" -ForegroundColor Yellow
try {
    $response = Invoke-RestMethod -Uri "$BaseUrl/clients?country_code=US" -Method Get
    $response | ConvertTo-Json -Depth 10
} catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
}
Write-Host ""

# Test 2: Get Canadian clients
Write-Host "2. Testing GET /clients?country_code=CA" -ForegroundColor Yellow
try {
    $response = Invoke-RestMethod -Uri "$BaseUrl/clients?country_code=CA" -Method Get
    $response | ConvertTo-Json -Depth 10
} catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
}
Write-Host ""

# Test 3: Get German clients
Write-Host "3. Testing GET /clients?country_code=DE" -ForegroundColor Yellow
try {
    $response = Invoke-RestMethod -Uri "$BaseUrl/clients?country_code=DE" -Method Get
    $response | ConvertTo-Json -Depth 10
} catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
}
Write-Host ""

# Test 4: Get Australian clients
Write-Host "4. Testing GET /clients?country_code=AU" -ForegroundColor Yellow
try {
    $response = Invoke-RestMethod -Uri "$BaseUrl/clients?country_code=AU" -Method Get
    $response | ConvertTo-Json -Depth 10
} catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
}
Write-Host ""

# Test 5: Get UK clients
Write-Host "5. Testing GET /clients?country_code=UK" -ForegroundColor Yellow
try {
    $response = Invoke-RestMethod -Uri "$BaseUrl/clients?country_code=UK" -Method Get
    $response | ConvertTo-Json -Depth 10
} catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
}
Write-Host ""

# Test 6: Test with invalid country code
Write-Host "6. Testing GET /clients?country_code=INVALID" -ForegroundColor Yellow
try {
    $response = Invoke-RestMethod -Uri "$BaseUrl/clients?country_code=INVALID" -Method Get
    $response | ConvertTo-Json -Depth 10
} catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
}
Write-Host ""

# Test 7: Test without country_code parameter
Write-Host "7. Testing GET /clients (without country_code parameter)" -ForegroundColor Yellow
try {
    $response = Invoke-RestMethod -Uri "$BaseUrl/clients" -Method Get
    $response | ConvertTo-Json -Depth 10
} catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
}
Write-Host ""

Write-Host "Testing completed!" -ForegroundColor Green 