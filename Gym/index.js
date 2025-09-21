const {app, BrowserWindow} = require('electron')

const creatWindow = () => {
    const win = new BrowserWindow({
        width:800,
        height:600,
    })

    win.loadURL("http://localhost:5048")
    win.setMenuBarVisibility(false)
}
app.whenReady().then(() => {
    creatWindow()
})