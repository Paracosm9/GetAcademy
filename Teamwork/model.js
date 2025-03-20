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
        motivationalQuotes: [ //motivation quotes should have id :) 
            { id: 101, text: "belive in santa." },
            { id: 102, text: "As Above So Below." },
            { id: 103, text: "heftig stå på." },
        ],

        friendships: [
            {user1: 1, user2: 2}
            {user1: 1, user2: 3}
            {user1: 1, user2: 4}
            {user1: 1, user2: 8}
            {user1: 1, user2: 9}
            {user1: 2, user2: 3}
            {user1: 2, user2: 4}
            {user1: 1, user2: 2}
            {user1: 1, user2: 2}
        ],
        
        users: [
            {
                id:1, userName: "RiktigUsername", role: "Admin", streak: 3, achievements: [{id: 1, dateAchieved: 'date'}], friends : [{id: 2}],
                id:2, userName: "LeedsForEver", role: "User", streak: , achievements: [{id: 3, dateAchieved: 'date'}], friends : [{id: 1}] 
                id:2, userName: "LokalSjappa", role: "User", streak: 5, achievements: [{id: 3, dateAchieved: 'date'}], friends : [{id: 1}] 
                
            }
        ],

        comments: [
            { id: 1001, user: "Ole", message: "randomquote" },
            { id: 1002, user: "Dag", message: "randomquote" },
            { id: 1003, user: "Kari", message: "randomquote" }
        ],

        drinks: [
            {id: 1, name: 'Redbull', volume: 330, coffeine: 10, img: 'redbull.png', price: 550}, 
            {id: 2, name: 'Kaffe', volume: 250, coffeine: 5, img: 'coffee.png', price: 250}, 
        ]
        
    }
}