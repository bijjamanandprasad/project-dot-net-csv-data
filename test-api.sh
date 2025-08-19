#!/bin/bash

# Test script for Client API
# Make sure the API is running before executing this script

BASE_URL="http://localhost:5000"

echo "Testing Client API endpoints..."
echo "================================"

# Test 1: Get US clients
echo "1. Testing GET /clients?country_code=US"
curl -s -X GET "$BASE_URL/clients?country_code=US" | jq '.'
echo -e "\n"

# Test 2: Get Canadian clients
echo "2. Testing GET /clients?country_code=CA"
curl -s -X GET "$BASE_URL/clients?country_code=CA" | jq '.'
echo -e "\n"

# Test 3: Get German clients
echo "3. Testing GET /clients?country_code=DE"
curl -s -X GET "$BASE_URL/clients?country_code=DE" | jq '.'
echo -e "\n"

# Test 4: Get Australian clients
echo "4. Testing GET /clients?country_code=AU"
curl -s -X GET "$BASE_URL/clients?country_code=AU" | jq '.'
echo -e "\n"

# Test 5: Get UK clients
echo "5. Testing GET /clients?country_code=UK"
curl -s -X GET "$BASE_URL/clients?country_code=UK" | jq '.'
echo -e "\n"

# Test 6: Test with invalid country code
echo "6. Testing GET /clients?country_code=INVALID"
curl -s -X GET "$BASE_URL/clients?country_code=INVALID" | jq '.'
echo -e "\n"

# Test 7: Test without country_code parameter
echo "7. Testing GET /clients (without country_code parameter)"
curl -s -X GET "$BASE_URL/clients" | jq '.'
echo -e "\n"

echo "Testing completed!" 