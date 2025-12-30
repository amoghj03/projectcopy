#!/bin/bash

echo "=== HRMS Database Setup ==="
echo ""

# Configuration
DB_HOST="localhost"
DB_PORT="5432"
DB_NAME="postgres"
DB_USER="postgres"
DB_PASSWORD="password"

echo "Database Configuration:"
echo "  Host: $DB_HOST"
echo "  Port: $DB_PORT"
echo "  Database: $DB_NAME"
echo "  User: $DB_USER"
echo ""

# Set PostgreSQL password environment variable
export PGPASSWORD="$DB_PASSWORD"

# Check if PostgreSQL is installed
echo "Checking PostgreSQL installation..."
if command -v psql >/dev/null 2>&1; then
    echo "✓ PostgreSQL found: $(psql --version)"
else
    echo "✗ PostgreSQL not found. Please install PostgreSQL first."
    echo "  https://www.postgresql.org/download/"
    exit 1
fi

# Check if PostgreSQL service is running (macOS / Linux)
echo "Checking PostgreSQL service..."
if pg_isready -h "$DB_HOST" -p "$DB_PORT" >/dev/null 2>&1; then
    echo "✓ PostgreSQL service is running"
else
    echo "✗ PostgreSQL service is not running"
    echo "  Start PostgreSQL and try again"
    exit 1
fi

# Test connection
echo "Testing database connection..."
if psql -h "$DB_HOST" -p "$DB_PORT" -U "$DB_USER" -d postgres -c "SELECT 1;" >/dev/null 2>&1; then
    echo "✓ Database connection successful"
else
    echo "✗ Cannot connect to PostgreSQL"
    exit 1
fi

# Check if database exists
echo "Checking if database '$DB_NAME' exists..."
DB_EXISTS=$(psql -h "$DB_HOST" -p "$DB_PORT" -U "$DB_USER" -d postgres -tAc "SELECT 1 FROM pg_database WHERE datname='$DB_NAME';")

if [ "$DB_EXISTS" = "1" ]; then
    echo "✓ Database '$DB_NAME' already exists"
    read -p "Do you want to drop and recreate it? (yes/no): " RESPONSE
    if [ "$RESPONSE" = "yes" ]; then
        echo "Dropping database '$DB_NAME'..."
        psql -h "$DB_HOST" -p "$DB_PORT" -U "$DB_USER" -d postgres -c "DROP DATABASE $DB_NAME;"
        echo "✓ Database dropped"

        echo "Creating database '$DB_NAME'..."
        psql -h "$DB_HOST" -p "$DB_PORT" -U "$DB_USER" -d postgres -c "CREATE DATABASE $DB_NAME;"
        echo "✓ Database created"
    fi
else
    echo "Creating database '$DB_NAME'..."
    psql -h "$DB_HOST" -p "$DB_PORT" -U "$DB_USER" -d postgres -c "CREATE DATABASE $DB_NAME;"
    if [ $? -eq 0 ]; then
        echo "✓ Database created successfully"
    else
        echo "✗ Failed to create database"
        exit 1
    fi
fi

# Run initialization script
SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
INIT_SCRIPT="$SCRIPT_DIR/init_db.sql"

if [ -f "$INIT_SCRIPT" ]; then
    echo "Running database initialization script..."
    psql -h "$DB_HOST" -p "$DB_PORT" -U "$DB_USER" -d "$DB_NAME" -f "$INIT_SCRIPT"
    if [ $? -eq 0 ]; then
        echo "✓ Database initialized successfully"
    else
        echo "✗ Failed to initialize database"
        exit 1
    fi
else
    echo "✗ Initialization script not found: $INIT_SCRIPT"
    exit 1
fi

# Verify tables
echo "Verifying tables..."
TABLE_COUNT=$(psql -h "$DB_HOST" -p "$DB_PORT" -U "$DB_USER" -d "$DB_NAME" -tAc "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema='public';")
echo "✓ Found $TABLE_COUNT tables in database"

echo ""
echo "=== Database Setup Complete ==="
echo ""
echo "Connection String:"
echo "  Host=$DB_HOST;Port=$DB_PORT;Database=$DB_NAME;Username=$DB_USER;Password=$DB_PASSWORD"
echo ""
echo "You can now run the .NET application!"
echo "  cd HRMS.Api"
echo "  dotnet run"
echo ""

# Clear password
unset PGPASSWORD
