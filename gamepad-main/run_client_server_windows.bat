CALL rmdir /s /q node_modules
CALL del package-lock.json
CALL npm install vite
CALL npm install
CALL npm start
