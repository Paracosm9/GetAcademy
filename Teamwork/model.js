const model = {
    app: {
        currentpage: 'dashboard',
    },

    inputs: {
        //for dashboard
       
        manageDrinks: {
            isEditingId: 0,
            editDrink: {
                name: '',
                volume: 0,
                coffeine: 0,
                price: 0,
                img: ''
            },
            addDrink: {
                name: '',
                volume: 0,
                coffeine: 0,
                price: 0,
                img: ''
            },
            
        },
    },

    data: {
        motivationalQuotes: [ 
            { id: 101, text: "belive in santa." },
            { id: 102, text: "As Above So Below." },
            { id: 103, text: "heftig stå på." },
        ],
        
        users: [
                {id:1, userName: "RiktigUsername", role: "Admin", streak: 3, achievements: [{id: 1, dateAchieved: 'date'}], friends: [2,3]},
                {id:2, userName: "LeedsForEver", role: "User", streak: 4, achievements: [{id: 3, dateAchieved: 'date'}], friends: [1,3]},
                {id:3, userName: "LokalSjappa", role: "User", streak: 5, achievements: [{id: 2, dateAchieved: 'date'}], friends: [1,2]}
        ],

        comments: [
            { id: 1001, userId: 1, message: "randomquote" },
            { id: 1002, userId: 2, message: "randomquote" },
            { id: 1003, userId: 3, message: "randomquote" }
        ],

        drinks: [
            {id: 1, name: 'Redbull', volume: 330, coffeine: 10, img: 'redbull.png', price: 550}, 
            {id: 2, name: 'Kaffe', volume: 250, coffeine: 5, img: 'coffee.png', price: 250} 
        ]      
    }
}