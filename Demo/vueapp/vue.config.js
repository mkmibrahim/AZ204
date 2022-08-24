const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
  transpileDependencies: true,
});
module.exports = {
  devServer: {
    // proxy: "https://localhost:5001"
    //proxy: process.env.BACKEND_API
    //proxy: "https://az204demobackendapp123.azurewebsites.net"
    }
}