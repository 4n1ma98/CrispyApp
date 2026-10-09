// CrispyApp JavaScript Utilities
window.crispyApp = {
    // Control limpio de tema sin eval
    setTheme: function (isDark) {
        if (isDark) {
            document.body.classList.remove('light-mode');
        } else {
            document.body.classList.add('light-mode');
        }
    },

    // Atajo global de tecla Enter para cobrar en la Caja
    initPosShortcuts: function () {
        document.addEventListener('keydown', function (event) {
            if (event.key === 'Enter') {
                // Ignorar si el foco está en un input de texto o cantidad
                if (event.target && event.target.tagName === 'INPUT') {
                    return;
                }
                var btn = document.getElementById('btnCobrar');
                if (btn && !btn.disabled) {
                    btn.click();
                }
            }
        });
    }
};

// Inicializar atajos al cargar el script
window.crispyApp.initPosShortcuts();
