export default interface Match {
    id: string,
    player1: string,
    player2: string,
    active: string | null,
    status: string,
    winnner: string | null,
    game: number[],
    playerTime: string | null,
    youPlay: boolean
}